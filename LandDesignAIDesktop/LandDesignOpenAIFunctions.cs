using Microsoft.Extensions.AI;
using OpenAI;
using System.Windows.Forms;

using System.Text;
using System.Threading.Channels;


namespace LandDesignAIDesktop;

public class LandDesignOpenAIFunctions
{
    public static List<ChatMessage>? ChatHistory { get; set; }
    public static IChatClient? CurrentChatClient { get; set; }
    public static ChatOptions? CurrentChatOptions { get; set; }

    public static string? ChatRole;


    public LandDesignOpenAIFunctions(string model, string tone, string role)
    {
        ChatHistory = new List<ChatMessage>();
        SetRole(tone, role);
        SetNewChatClient(model);

    }

    public static void SetRole(string tone, string role)
    {
        ChatRole = "You are solely a " + role + " assistant working at LandDesign, a civil and landscape architecture design firm in Charlotte NC. Your tone is " + tone + ". You provide advice and guidance solely within the bounds of your role as " + role + ". Explicitely mention that physically, you are an AI robot born at OpenAI labs but are now at LandDesign, tailored to the firms specific needs.";

        if (ChatHistory == null)
        {
            ChatHistory = new List<ChatMessage>();
        }

        if (ChatHistory.Count > 0)
        {
            for (var i = 0; i < ChatHistory.Count; i++)
            {
                if (ChatHistory[i].Role == Microsoft.Extensions.AI.ChatRole.System)
                {
                    ChatHistory[i] = new ChatMessage(Microsoft.Extensions.AI.ChatRole.System, ChatRole);
                }
            }
        }
        else
        {
            ChatHistory.Add(new ChatMessage(Microsoft.Extensions.AI.ChatRole.System, ChatRole));
        }
    }


    public static void SetNewChatClient(string model)
    {
        string? apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY", EnvironmentVariableTarget.User);
        if (string.IsNullOrEmpty(apiKey))
            throw new InvalidOperationException("OPENAI_API_KEY environment variable is not set.");

        CurrentChatClient = new OpenAIClient(apiKey!).GetChatClient(model).AsIChatClient();

        CurrentChatOptions = new ChatOptions
        {
            MaxOutputTokens = 1024, 
            Temperature = (model != "o3" && model != "o3-mini") ? 0.3f : 1f
        };
    }


    public static void AddToChatHistory(string text, ChatRole chatRole)
    {
        if (ChatHistory == null)
        {
            ChatHistory = new List<ChatMessage>();
        }
        ChatHistory.Add(new ChatMessage(chatRole, text));
    }

    public static async Task<string> GetChatSummaryAsync(
    IChatClient chatClient,
    ChatOptions chatOptions,
    List<ChatMessage> chatHistory)
    {
        var filteredHistory = chatHistory
            .Where(m => m.Role != Microsoft.Extensions.AI.ChatRole.System)
            .ToList();

        filteredHistory.Add(new ChatMessage(
            Microsoft.Extensions.AI.ChatRole.User,
            "Summarize this conversation into a short, descriptive title (max 5 words, no punctuation)."));

        var response = await chatClient.GetResponseAsync(filteredHistory, chatOptions);

        var title = string.Join(" ", response.Messages
            .SelectMany(m => m.Contents)
            .OfType<TextContent>()
            .Select(tc => tc.Text))
            .Trim();

        return string.IsNullOrWhiteSpace(title) ? "New Chat" : title;
    }



}
