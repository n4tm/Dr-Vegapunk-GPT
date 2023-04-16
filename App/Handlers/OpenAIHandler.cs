using OpenAI_API;
using OpenAI_API.Chat;
using OpenAI_API.Images;

namespace DrVegapunk.GPT.App.Managers;

public class OpenAIHandler {
    private static readonly OpenAIAPI _api = new();
    private readonly Conversation? _chat = _api.Chat.CreateConversation();

    public async IAsyncEnumerable<string> GetGPTStreamOutputAsync(string input) {
        _chat!.AppendUserInput(input);

        await foreach (var ans in _chat.StreamResponseEnumerableFromChatbotAsync()) {
            yield return ans;
        }
    }

    public async Task<string> GetGPTOutputAsync(string input) {
        _chat!.AppendUserInput(input);
        return await _chat.GetResponseFromChatbotAsync();
    }

    public async Task<ImageResult> GetDallEOutputAsync(string input) {
        _chat!.AppendUserInput(input);
        return await _api.ImageGenerations.CreateImageAsync(input);
    }
}