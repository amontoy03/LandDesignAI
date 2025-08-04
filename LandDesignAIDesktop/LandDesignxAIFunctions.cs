using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace LandDesignAIDesktop
{
    public sealed class LandDesignxAIFunctions : IDisposable
    {
        private readonly HttpClient _http;

        // Supply your API key securely.
        public LandDesignxAIFunctions(string apiKey, HttpMessageHandler? handler = null, Uri? baseUri = null)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
                throw new ArgumentNullException(nameof(apiKey));

            _http = handler is null ? new HttpClient() : new HttpClient(handler, disposeHandler: false);
            _http.BaseAddress = baseUri ?? new Uri("https://api.x.ai/v1/");

            _http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", apiKey);
        }

        /// <summary>
        /// Send a chat completion request and return the assistant's first message string.
        /// Pass the full running conversation (stateless server).
        /// </summary>
        public async Task<string> CreateChatCompletionAsync(
            IList<XaiChatMessage> messages,
            string model = "grok-3",
            int? maxTokens = null,
            double? temperature = null,
            CancellationToken cancellationToken = default)
        {
            if (messages is null || messages.Count == 0)
                throw new ArgumentException("At least one message is required.", nameof(messages));

            var req = new XaiChatCompletionRequest
            {
                Model = model,
                Messages = messages,
                MaxTokens = maxTokens,
                Temperature = temperature
            };

            using var content = new StringContent(
                JsonSerializer.Serialize(req, JsonOptions),
                Encoding.UTF8,
                "application/json");

            using var resp = await _http.PostAsync("chat/completions", content, cancellationToken).ConfigureAwait(false);

            // Throw if non-success and include body for diagnostics.
            var body = await resp.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
            if (!resp.IsSuccessStatusCode)
            {
                throw new HttpRequestException(
                    $"xAI Chat error {(int)resp.StatusCode} {resp.ReasonPhrase}: {body}",
                    null,
                    resp.StatusCode);
            }

            var result = JsonSerializer.Deserialize<XaiChatCompletionResponse>(body, JsonOptions)
                         ?? throw new InvalidOperationException("Null/invalid chat completion response.");

            // Defensive: choices array may be empty.
            var msg = result.Choices is { Length: > 0 } ? result.Choices[0].Message?.Content : null;
            return msg ?? string.Empty;
        }

        public void Dispose() => _http.Dispose();

        // Shared System.Text.Json options.
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower, // .NET 9; if before .NET 9, map manually or use attributes.
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };
    }

    // ==== Request/Response POCOs ====

    public sealed class XaiChatCompletionRequest
    {
        [JsonPropertyName("model")]
        public string Model { get; set; } = "";

        [JsonPropertyName("messages")]
        public IList<XaiChatMessage> Messages { get; set; } = [];

        [JsonPropertyName("max_tokens")]
        public int? MaxTokens { get; set; }

        [JsonPropertyName("temperature")]
        public double? Temperature { get; set; }

        // Add top_p, stream, search_parameters, etc. as needed.
    }

    public sealed class XaiChatMessage
    {
        [JsonPropertyName("role")]
        public string Role { get; set; } = ""; // "system" | "user" | "assistant" | tool etc.

        [JsonPropertyName("content")]
        public string Content { get; set; } = "";
    }

    public sealed class XaiChatCompletionResponse
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("object")]
        public string? Object { get; set; }

        [JsonPropertyName("created")]
        public long? Created { get; set; }

        [JsonPropertyName("model")]
        public string? Model { get; set; }

        [JsonPropertyName("choices")]
        public XaiChatChoice[]? Choices { get; set; }

        [JsonPropertyName("usage")]
        public XaiUsage? Usage { get; set; }
    }

    public sealed class XaiChatChoice
    {
        [JsonPropertyName("index")]
        public int? Index { get; set; }

        [JsonPropertyName("message")]
        public XaiChatMessage? Message { get; set; }

        // If streaming, you'll see delta objects; extend as needed.
        [JsonPropertyName("finish_reason")]
        public string? FinishReason { get; set; }
    }

    public sealed class XaiUsage
    {
        [JsonPropertyName("prompt_tokens")]
        public int? PromptTokens { get; set; }

        [JsonPropertyName("completion_tokens")]
        public int? CompletionTokens { get; set; }

        [JsonPropertyName("total_tokens")]
        public int? TotalTokens { get; set; }
    }
}
