// AI abstractions + OpenAI extensions

using LandDesignAIDesktop.Properties;
using MaterialSkin;
using MaterialSkin.Controls;
using Microsoft.Extensions.AI;
using NAudio.CoreAudioApi;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
// ChatMessage, ChatRole, IChatClient, ChatOptions, ChatResponse
// GetChatClient(..) extension
using Timer = System.Windows.Forms.Timer;

namespace LandDesignAIDesktop.Forms
{
    public partial class Form1 : MaterialForm
    {
        private readonly Timer _timer = new() { Interval = 100 };
        private MMDevice? _mic;
        private readonly LandDesignAIVoice _voice;
        private AISqliteFunctions? _sqliteFunctionsFunctions;
        private Dictionary<TabPage, List<ChatMessage>> _chatHistories = new();


        public Form1()
        {
            InitializeComponent();

            // Configure flowLayoutPanel1 behavior
            flowLayoutPanel1.WrapContents = false;
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.Dock = DockStyle.Fill;

            // MaterialSkin configuration
            var skin = MaterialSkinManager.Instance;
            skin.AddFormToManage(this);
            skin.Theme = MaterialSkinManager.Themes.DARK;
            skin.ColorScheme = new ColorScheme(Primary.Grey800, Primary.Grey900, Primary.Grey700, Accent.Lime700,
                TextShade.WHITE);

            // Control configuration
            materialComboBox_Tone.Margin = new Padding(0, 0, 8, 0);
            materialComboBox_Role.Margin = new Padding(0, 0, 8, 0);
            materialButton_AddFiles.Icon = Resources.plus;
            materialButton_AddFiles.AutoSize = true;
            materialButton_Talk.Icon = Resources.mic;

            // Icon and TabControl setup
            var icons = new ImageList
            {
                ColorDepth = ColorDepth.Depth32Bit,
                ImageSize = new Size(24, 24)
            };
            icons.Images.Add("audio", Properties.Resources.audio);
            icons.Images.Add("NewChat", Properties.Resources.square_pen);
            icons.Images.Add("CurrentChat", Properties.Resources.chevron);
            materialTabControl_Left.ImageList = icons;
            materialTabControl_Left.TabPages[0].ImageKey = "audio";
            materialTabControl_Left.TabPages[1].ImageKey = "NewChat";
            materialTabControl_Left.TabPages[2].ImageKey = "CurrentChat";
            materialTabControl_Left.SelectedTab = materialTabControl_Left.TabPages[2];

            // Voice and AI initialization
            _voice = new LandDesignAIVoice();
            _voice.TranscriptReady += Voice_TranscriptReady;
            _voice.AssistantTextReady += Voice_AssistantTextReady;
            new LandDesignOpenAIFunctions(materialComboBox_Model.Text, materialComboBox_Tone.Text,
                materialComboBox_Role.Text);



        }

        private async Task<ChatResponse> GetChatResponse(string promptText)
        {
            var chat = LandDesignOpenAIFunctions.CurrentChatClient;
            if (chat == null)
                throw new InvalidOperationException("CurrentChatClient is not initialized.");

            var history = GetCurrentChatHistory();

            var reply = await chat.GetResponseAsync(
                history,
                LandDesignOpenAIFunctions.CurrentChatOptions);

            return reply;
        }

        private async Task StreamChatResponseAsync(ChatBubble bubble, CancellationToken token)
        {
            var chat = LandDesignOpenAIFunctions.CurrentChatClient;
            if (chat == null)
                throw new InvalidOperationException("CurrentChatClient is not initialized.");

            var options = LandDesignOpenAIFunctions.CurrentChatOptions;
            var tab = materialTabControl_Left.SelectedTab;
            List<ChatMessage> history;

            if (tab != null && _chatHistories.TryGetValue(tab, out var h))
            {
                history = h;
            }
            else
            {
                history = new List<ChatMessage>();
            }

            var sb = new StringBuilder();

            await foreach (var update in chat.GetStreamingResponseAsync(history, options, token))
            {
                foreach (var content in update.Contents)
                {
                    switch (content)
                    {
                        case TextContent t:
                            sb.Append(t.Text);

                            var panel = GetCurrentChatPanel();
                            if (panel.Controls.Count > 0)
                            {
                                panel.ScrollControlIntoView(panel.Controls[panel.Controls.Count - 1]);
                            }
                            break;

                        case TextReasoningContent r:
                            break;
                    }
                }

                bubble.BeginInvoke(() => bubble.UpdateText(sb.ToString()));
            }

            LandDesignOpenAIFunctions.AddToChatHistory(sb.ToString(), ChatRole.Assistant);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _sqliteFunctionsFunctions = new AISqliteFunctions();

            var chatData = _sqliteFunctionsFunctions.LoadMessagesByChatId();

            foreach (var kvp in chatData)
            {
                string chatName = kvp.Key;
                var messages = kvp.Value;

                var tab = new TabPage(chatName);
                var chatPanel = new ChatPanel { Dock = DockStyle.Fill };
                tab.Controls.Add(chatPanel);
                materialTabControl_Left.TabPages.Add(tab);

                _chatHistories[tab] = new List<ChatMessage>();

                foreach (var (role, message) in messages)
                {
                    var bubbleSender = role == "assistant" ? ChatBubble.Sender.Bot : ChatBubble.Sender.User;
                    chatPanel.ChatFlow.Controls.Add(new ChatBubble(message, bubbleSender, chatPanel.Width));

                    var chatRole = role.ToLower() switch
                    {
                        "user" => Microsoft.Extensions.AI.ChatRole.User,
                        "assistant" => Microsoft.Extensions.AI.ChatRole.Assistant,
                        "system" => Microsoft.Extensions.AI.ChatRole.System,
                        _ => Microsoft.Extensions.AI.ChatRole.User
                    };

                    _chatHistories[tab].Add(new ChatMessage(chatRole, message));
                }

                chatPanel.SendMessageRequested += (s, message) => HandleUserMessage(chatPanel, message);
                chatPanel.AddFilesRequested += (s, e2) => HandleAddFiles();
            }
        }

        private async void UpdateTabTitle(TabPage tab)
        {
            if (!_chatHistories.ContainsKey(tab) || _chatHistories[tab].Count < 2)
                return;

            string summaryTitle = await LandDesignOpenAIFunctions.GetChatSummaryAsync(
                LandDesignOpenAIFunctions.CurrentChatClient!,
                LandDesignOpenAIFunctions.CurrentChatOptions!,
                _chatHistories[tab]);

            if (!string.IsNullOrWhiteSpace(summaryTitle) &&
                (tab.Text.StartsWith("Generating") || tab.Text.Contains("Chat")))
            {
                tab.Text = summaryTitle;
            }
        }

        private LandDesignxAIFunctions? _xai;
        private void materialComboBox_Model_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (materialComboBox_Model.SelectedText.Contains("grok"))
            {
                var apiKey = Environment.GetEnvironmentVariable("XAI_API_KEY"); // or secure config
                _xai = new LandDesignxAIFunctions(apiKey!);
            }

            LandDesignOpenAIFunctions.SetNewChatClient(materialComboBox_Model.Text);
        }

        private void materialComboBox_Tone_SelectedIndexChanged(object sender, EventArgs e)
        {
            LandDesignOpenAIFunctions.SetRole(materialComboBox_Tone.Text, materialComboBox_Role.Text);
        }

        private void materialComboBox_Role_SelectedIndexChanged(object sender, EventArgs e)
        {
            LandDesignOpenAIFunctions.SetRole(materialComboBox_Tone.Text, materialComboBox_Role.Text);
        }

        private async void materialTextBox_PromptBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter || string.IsNullOrWhiteSpace(materialTextBox_PromptBox.Text))
                return;

            e.SuppressKeyPress = true;

            var tab = tabPage_ThisChat;

            // Ensure This Chat has a history entry
            if (!_chatHistories.ContainsKey(tab))
                _chatHistories[tab] = new List<ChatMessage>();

            // Add system message if missing
            if (!_chatHistories[tab].Any(m => m.Role == Microsoft.Extensions.AI.ChatRole.System))
            {
                _chatHistories[tab].Insert(0,
                    new ChatMessage(Microsoft.Extensions.AI.ChatRole.System, LandDesignOpenAIFunctions.ChatRole));
            }

            var promptText = materialTextBox_PromptBox.Text;

            if (materialSwitch_SpellingCorrection.Checked)
            {
                promptText = await SpellChecker.CorrectAsync(promptText);
            }

            var panel = GetCurrentChatPanel();

            var userBubble = new ChatBubble(promptText, ChatBubble.Sender.User, panel.ClientSize.Width);
            panel.Controls.Add(userBubble);

            _chatHistories[tab].Add(new ChatMessage(Microsoft.Extensions.AI.ChatRole.User, promptText));

            materialTextBox_PromptBox.Clear();

            // Progress bar
            var newBar = new MaterialProgressBar
            {
                Maximum = 100,
                Value = 0,
                Width = panel.Width - 20,
                Height = 10,
                Margin = new Padding(8)
            };
            panel.Controls.Add(newBar);

            // Bot bubble
            var botBubble = new ChatBubble(string.Empty, ChatBubble.Sender.Bot, panel.Width);
            panel.Controls.Add(botBubble);

            // Progress animation
            CancellationTokenSource cts = new();
            var token = cts.Token;
            _ = Task.Run(async () =>
            {
                while (!token.IsCancellationRequested)
                {
                    for (int i = 0; i <= 100; i += 5)
                    {
                        if (token.IsCancellationRequested) break;
                        newBar.Invoke(() => newBar.Value = i);
                        await Task.Delay(50, token);
                    }
                }
            }, token);

            using var cts2 = new CancellationTokenSource();
            try
            {
                await StreamChatResponseAsync(botBubble, cts2.Token);
            }
            catch (OperationCanceledException) { }

            // Stop progress bar
            cts.Cancel();
            panel.Controls.Remove(newBar);
            newBar.Dispose();

            // Add AI reply to history
            _chatHistories[tab].Add(new ChatMessage(Microsoft.Extensions.AI.ChatRole.Assistant, botBubble.Text));

            // Generate summary title
            if (_chatHistories[tab].Count >= 2)
            {
                string summaryTitle = await LandDesignOpenAIFunctions.GetChatSummaryAsync(
                    LandDesignOpenAIFunctions.CurrentChatClient!,
                    LandDesignOpenAIFunctions.CurrentChatOptions!,
                    _chatHistories[tab]);

                if (!string.IsNullOrWhiteSpace(summaryTitle) &&
                    (tab.Text.StartsWith("Generating") || tab.Text.Contains("Chat")))
                {
                    tab.Text = summaryTitle;
                }
            }
        }

        private void materialButton_AddFiles_Click(object sender, EventArgs e)
        {
            string filePath;

            OpenFileDialog thisDialog = new OpenFileDialog();
            thisDialog.Title = "Select File To Upload";
            thisDialog.DefaultExt = ".pdf";

            DialogResult result = thisDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                DialogResult warningResult =
                    MessageBox.Show(
                        "AI can expose file data to the public. No sensitive files regarding confidential projects or clients should be uploaded. Proceed?",
                        "Warning", MessageBoxButtons.OKCancel);

                if (warningResult == DialogResult.OK)
                {
                    filePath = thisDialog.FileName;
                }
            }
        }

        private void materialSwitch_SpellingCorrection_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void materialTabControl_Left_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage == tabPage_NewChat)
            {
                CreateNewChatTab();
            }
        }

        private void materialTabControl_Left_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pictureBoxSettings_Click(object sender, EventArgs e)
        {

        }

        private void Voice_TranscriptReady(object? sender, string transcript)
        {
            if (InvokeRequired)
            {
                Invoke(() => Voice_TranscriptReady(sender, transcript));
                return;
            }

            var userBubble = new ChatBubble(transcript, ChatBubble.Sender.User, flowLayoutPanel_Voice.Width - 20);
            flowLayoutPanel_Voice.Controls.Add(userBubble);
            flowLayoutPanel_Voice.ScrollControlIntoView(userBubble);
        }

        private void Voice_AssistantTextReady(object? sender, string response)
        {
            if (InvokeRequired)
            {
                Invoke(() => Voice_AssistantTextReady(sender, response));
                return;
            }

            var aiBubble = new ChatBubble(response, ChatBubble.Sender.Bot, flowLayoutPanel_Voice.Width - 20);
            flowLayoutPanel_Voice.Controls.Add(aiBubble);
            flowLayoutPanel_Voice.ScrollControlIntoView(aiBubble);
        }

        private async void materiaButton_mic_MouseDown(object? sender, MouseEventArgs e)
        {
            label_userVolume.Visible = true;
            volumeMeter_User.Visible = true;

            materialButton_Talk.Text = "Listening...";
            // --- grab the default communications microphone --------------------
            _mic = new MMDeviceEnumerator()
                .GetDefaultAudioEndpoint(DataFlow.Capture, Role.Communications);

            // 2. Poll its built-in peak meter on the UI thread
            _timer.Tick += (_, __) =>
            {
                float peak = 0f;
                try
                {
                    peak = _mic?.AudioMeterInformation.MasterPeakValue ?? 0f; // 0..1
                    volumeMeter_User.Amplitude = peak;
                    label_userVolume.Text = Math.Round(peak, 2) + " Db";
                }
                catch (ObjectDisposedException)
                {
                    /* device vanished – keep 0 */
                }
            };
            _timer.Start();

            await _voice.BeginRecordingAsync();
        }

        private async void materialFloatingActionButton_mic_MouseUp(object? sender, MouseEventArgs e)
        {
            volumeMeter_User.Visible = false;
            label_userVolume.Visible = false;
            label_userVolume.Text = "Initiating...";

            materialButton_Talk.Text = "Processing...";

            _timer.Stop();
            _mic?.Dispose();
            _mic = null;
            volumeMeter_User.Amplitude = 0;

            await _voice.EndRecordingAndSendAsync(); // stop + send to GPT-4o
            materialButton_Talk.Text = "Push to Talk";
        }



        // Update the method signature to match the delegate's nullability
        private async void flowLayoutPanel1_ControlAdded(object? sender, ControlEventArgs e)
        {
            if (sender is not FlowLayoutPanel panel)
                return;

            // Get the TabPage this panel belongs to
            var tab = materialTabControl_Left.TabPages
                .Cast<TabPage>()
                .FirstOrDefault(t => t.Controls.Contains(panel));

            if (tab == null || !_chatHistories.ContainsKey(tab))
                return;

            var history = _chatHistories[tab];
            if (history.Count < 2)
                return;

            // Build a plain text version of the conversation
            string allText = string.Join("\n", history.Select(m => $"{m.Role}: {m.Contents}"));

            // Call GPT to generate a summary title
            string summaryTitle = await LandDesignOpenAIFunctions.GetChatSummaryAsync(
                LandDesignOpenAIFunctions.CurrentChatClient!,
                LandDesignOpenAIFunctions.CurrentChatOptions!,
                _chatHistories[tab]);

            tab.Text = summaryTitle;
        }

        private void CreateNewChatTab()
        {
            var newTab = new TabPage("New Chat");
            var chatPanel = new ChatPanel();
            chatPanel.Dock = DockStyle.Fill;


            chatPanel.SelectThisChatTab();


            chatPanel.SendMessageRequested += (s, message) => HandleUserMessage(chatPanel, message);
            chatPanel.AddFilesRequested += (s, e) => HandleAddFiles();

            newTab.Controls.Add(chatPanel);
            materialTabControl_Left.TabPages.Add(newTab);
            materialTabControl_Left.SelectedTab = newTab;
        }


        private FlowLayoutPanel GetCurrentChatPanel()
        {
            var tab = materialTabControl_Left.SelectedTab;

            if (tab?.Controls[0] is ChatPanel cp)
            {

                var panel = cp.Controls
                              .OfType<TableLayoutPanel>()
                              .SelectMany(t => t.Controls.OfType<FlowLayoutPanel>())
                              .FirstOrDefault();

                if (panel != null)
                    return panel;
            }


            return flowLayoutPanel1;
        }

        private void HandleAddFiles()
        {
            using var dialog = new OpenFileDialog
            {
                Title = "Select File To Upload",
                DefaultExt = ".pdf",
                Filter = "PDF Files (*.pdf)|*.pdf|All Files (*.*)|*.*"
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var confirm = MessageBox.Show(
                    "AI can expose file data to the public. No sensitive files should be uploaded. Proceed?",
                    "Warning", MessageBoxButtons.OKCancel);

                if (confirm == DialogResult.OK)
                {
                    string filePath = dialog.FileName;
                    MessageBox.Show($"File selected: {filePath}", "File Uploaded");
                }
            }
        }

        private List<ChatMessage> GetCurrentChatHistory()
        {
            var tab = materialTabControl_Left.SelectedTab;
            if (tab == null)
                return new List<ChatMessage>();
            return _chatHistories.TryGetValue(tab, out var history)
                ? history
                : new List<ChatMessage>();
        }

        private async void HandleUserMessage(ChatPanel chatPanel, string message)
        {
            var tab = materialTabControl_Left.SelectedTab;
            if (tab == null) return;

            // ✅ Step 2 check: Ensure Chat Client is initialized
            if (LandDesignOpenAIFunctions.CurrentChatClient == null)
            {
                MessageBox.Show("Chat client is not initialized.");
                return;
            }

            LandDesignOpenAIFunctions.CurrentChatOptions = new ChatOptions
            {
                MaxOutputTokens = 500,
                Temperature = (materialComboBox_Model.Text != "o3" && materialComboBox_Model.Text != "o3-mini") ? 0.7f : 1f
            };

            if (!_chatHistories.ContainsKey(tab))
            {
                _chatHistories[tab] = new List<ChatMessage>();
            }

            if (!_chatHistories[tab].Any(m => m.Role == Microsoft.Extensions.AI.ChatRole.System))
            {
                _chatHistories[tab].Insert(0,
                    new ChatMessage(Microsoft.Extensions.AI.ChatRole.System, LandDesignOpenAIFunctions.ChatRole));
            }

            var userBubble = new ChatBubble(message, ChatBubble.Sender.User, chatPanel.Width);
            chatPanel.ChatFlow.Controls.Add(userBubble);

            _chatHistories[tab].Add(new ChatMessage(Microsoft.Extensions.AI.ChatRole.User, message));

            tab.Text = "Generating title…";

            var botBubble = new ChatBubble(string.Empty, ChatBubble.Sender.Bot, chatPanel.Width);
            chatPanel.ChatFlow.Controls.Add(botBubble);

            try
            {
                var sb = new StringBuilder();
                await foreach (var chunk in LandDesignOpenAIFunctions.CurrentChatClient!
                    .GetStreamingResponseAsync(_chatHistories[tab], LandDesignOpenAIFunctions.CurrentChatOptions!, CancellationToken.None))
                {
                    foreach (var content in chunk.Contents)
                    {
                        if (content is TextContent text)
                        {
                            sb.Append(text.Text);
                            botBubble.BeginInvoke(() => botBubble.UpdateText(sb.ToString()));
                        }
                    }
                }

                _chatHistories[tab].Add(new ChatMessage(Microsoft.Extensions.AI.ChatRole.Assistant, sb.ToString()));

                if (_chatHistories[tab].Count >= 2)
                {
                    string summaryTitle = await LandDesignOpenAIFunctions.GetChatSummaryAsync(
                        LandDesignOpenAIFunctions.CurrentChatClient!,
                        LandDesignOpenAIFunctions.CurrentChatOptions!,
                        _chatHistories[tab]);

                    if (!string.IsNullOrWhiteSpace(summaryTitle) &&
                        (tab.Text.StartsWith("Generating") || tab.Text.Contains("Chat")))
                    {
                        tab.Text = summaryTitle;
                    }
                }
            }
            catch (OperationCanceledException)
            {
                // ignore cancel
            }
        }


    }
}