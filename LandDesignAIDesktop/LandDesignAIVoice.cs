// LandDesignAIVoice.cs – v2.2.0-compatible
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using NAudio.Wave;
using OpenAI;
using OpenAI.RealtimeConversation;
using System;
using System.ClientModel;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

#pragma warning disable SKEXP0070
#pragma warning disable OPENAI002

namespace LandDesignAIDesktop;

public sealed class LandDesignAIVoice : IDisposable            
{
    public event EventHandler<string>? TranscriptReady;
    public event EventHandler<string>? AssistantTextReady;
    public event EventHandler<byte[]>? AssistantAudioReady;

    private const string ChatModel = "gpt-4o-realtime-preview";
    private const string WhisperModel = "whisper-1";
    private const int SampleRateHz = 24_000;
    private const short BitsPerSample = 16;
    private const short Channels = 1;

    private readonly WaveInEvent _waveIn;
    private readonly MemoryStream _captureStream = new();
    private readonly SemaphoreSlim _sendLock = new(1, 1);
    private readonly RealtimeConversationSession _session;
    private readonly ConcurrentDictionary<string, MemoryStream> _audioByItem = new();

    private readonly ConcurrentDictionary<string, StringBuilder> _textByItem = new();   // NEW

    private bool _isRecording;

    public LandDesignAIVoice(string? apiKey = null)
    {
        apiKey ??= Environment.GetEnvironmentVariable("OPENAI_API_KEY")
                 ?? throw new InvalidOperationException("API key not provided and OPENAI_API_KEY not set.");

        Kernel.CreateBuilder()
              .AddOpenAIChatCompletion(ChatModel, apiKey)
              .Build();

        var client = new RealtimeConversationClient(ChatModel, new ApiKeyCredential(apiKey));
        _session = client.StartConversationSessionAsync().GetAwaiter().GetResult();

        _session.ConfigureSessionAsync(new ConversationSessionOptions
        {
            Voice = ConversationVoice.Echo,
            InputAudioFormat = ConversationAudioFormat.Pcm16,
            OutputAudioFormat = ConversationAudioFormat.Pcm16,
            InputTranscriptionOptions = new() { Model = WhisperModel }
        }).Wait();

        _session.AddItemAsync(ConversationItem.CreateSystemMessage(["You are a helpful assistant. Respond clearly and concisely."])).Wait();

        _ = Task.Run(ConsumeUpdatesAsync);

        _waveIn = new WaveInEvent
        {
            WaveFormat = new WaveFormat(SampleRateHz, BitsPerSample, Channels)
        };
        _waveIn.DataAvailable += OnWaveInDataAvailable;
    }

    private void OnWaveInDataAvailable(object? sender, WaveInEventArgs e)
    {
        if (_isRecording)
            _captureStream.Write(e.Buffer, 0, e.BytesRecorded);
    }

    public Task BeginRecordingAsync()
    {
        if (_isRecording) return Task.CompletedTask;
        _captureStream.SetLength(0);
        _waveIn.StartRecording();
        _isRecording = true;
        return Task.CompletedTask;
    }

    public async Task EndRecordingAndSendAsync()
    {
        if (!_isRecording) return;
        _waveIn.StopRecording();
        _isRecording = false;

        byte[] pcm = _captureStream.ToArray();

        await _sendLock.WaitAsync();
        try
        {
            await _session.SendInputAudioAsync(new MemoryStream(pcm));
        }
        finally
        {
            _sendLock.Release();
        }
    }
    // ---------------------------------------------------------------------
    //  Session update pump  – v2.2.0 of OpenAI.RealtimeConversation
    // ---------------------------------------------------------------------

    private async Task ConsumeUpdatesAsync()
    {
        await foreach (ConversationUpdate update in _session.ReceiveUpdatesAsync())
        {
            switch (update)
            {
                // -------- user transcription ---------------------------------------
                case ConversationInputTranscriptionFinishedUpdate tr:
                    TranscriptReady?.Invoke(this, tr.Transcript);
                    break;

                // -------- assistant streaming delta  (audio) -----------------------
                case ConversationItemStreamingPartDeltaUpdate delta
                    when delta.AudioBytes is { Length: > 0 }:
                    _audioByItem
                        .GetOrAdd(delta.ItemId, CreateAudioStream)
                        .Write(delta.AudioBytes);
                    break;

                // -------- assistant streaming delta  (partial text) ---------------
                case ConversationItemStreamingPartDeltaUpdate delta
                    when !string.IsNullOrEmpty(delta.AudioTranscript):
                    AssistantTextReady?.Invoke(this, delta.AudioTranscript);

                    // accumulate for final message
                    _textByItem
                        .GetOrAdd(delta.ItemId, CreateTextBuilder)
                        .Append(delta.AudioTranscript);
                    break;

                // -------- assistant item finished ----------------------------------
                case ConversationItemStreamingFinishedUpdate fin:
                    {
                        // play / emit audio (if any)
                        if (_audioByItem.TryRemove(fin.ItemId, out var pcmStream))
                        {
                            byte[] allPcm = pcmStream.ToArray();
                            AssistantAudioReady?.Invoke(this, allPcm);
                            _ = Task.Run(delegate { PlayPcm(allPcm); });
                            pcmStream.Dispose();
                        }

                        // emit the final consolidated text
                        if (_textByItem.TryRemove(fin.ItemId, out var sb)
                            && sb.Length > 0)
                        {
                            AssistantTextReady?.Invoke(this, sb.ToString());
                        }

                        break;
                    }

                // -------- errors ---------------------------------------------------
                case ConversationErrorUpdate err:
                    Debug.WriteLine($"[LandDesignAIVoice] ERROR: {err.Message}");
                    break;
            }
        }
    }

    private MemoryStream CreateAudioStream(string itemId)
    {
        return new MemoryStream();
    }

    private StringBuilder CreateTextBuilder(string itemId)
    {
        return new StringBuilder();
    }

    private static void PlayPcm(byte[] pcm)
    {
        var fmt = new WaveFormat(SampleRateHz, BitsPerSample, Channels);
        using var stream = new RawSourceWaveStream(new MemoryStream(pcm), fmt);
        using var wo = new WaveOutEvent();
        wo.Init(stream);
        wo.Play();
        while (wo.PlaybackState == PlaybackState.Playing)
            Thread.Sleep(50);
    }

    // ---- CHANGED: synchronous dispose -------------------------------------
    public void Dispose()                                               // CHANGED
    {
        _waveIn?.Dispose();
        _session?.Dispose();                                            // CHANGED
        _captureStream.Dispose();
        _sendLock.Dispose();
    }
}