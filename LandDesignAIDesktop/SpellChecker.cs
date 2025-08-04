using Microsoft.Extensions.AI;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LandDesignAIDesktop
{
    /// <summary>
    /// Static helper that turns "Helo. How arre you?" → "Hello. How are you?" using OpenAI.
    /// </summary>
    public static class SpellChecker
    {
        /// <summary>
        /// Returns the input string with obvious spelling mistakes fixed by OpenAI.
        /// </summary>
        public static async Task<string> CorrectAsync(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text ?? string.Empty;

            var client = LandDesignOpenAIFunctions.CurrentChatClient;
            var options = LandDesignOpenAIFunctions.CurrentChatOptions;

            if (client == null)
                throw new InvalidOperationException("OpenAI client is not initialized.");

            var messages = new List<ChatMessage>
            {
                new(ChatRole.System, "You are a spell corrector. Fix all spelling mistakes and return corrected plain text. Do not explain."),
                new(ChatRole.User, text)
            };

            var result = await client.GetResponseAsync(messages, options);
            return result.Text.Trim();
        }
    }
}