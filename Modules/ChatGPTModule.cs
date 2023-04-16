using Discord;
using Discord.Commands;
using DrVegapunk.GPT.App;
using DrVegapunk.GPT.App.Managers;
using DrVegapunk.GPT.Modules;

namespace DrVegapunk.GPT.Commands;
public class ChatGPTModule : OpenAIModule {
    public ChatGPTModule(OpenAIHandler openAiHandler) : base(openAiHandler) { }

    [Command("say")]
    [Summary("Requests an answer from ChatGPT")]
    public async Task ReplyChatGPTOutput([Remainder] string input) {
        if (!await IsInputValidAsync(input)) return;

        var msgRef = new MessageReference(Context.Message.Id);

        if (BotConfig._.EnableGPTStreamOutput) {
            await HandleGPTStreamOutput(input, msgRef);
        } else {
            await HandleGPTBasicOutput(input, msgRef);
        }
    }

    private async Task HandleGPTBasicOutput(string input, MessageReference msgRef) {
        var ans = await _openAIHandler.GetGPTOutputAsync(input);
        if (ans?.Length >= BotConfig._.IOMaxLength) {
            await ReplyAsync(GetIOMaxLenInfo(), messageReference: msgRef);
            return;
        }
        await ReplyAsync(ans, messageReference: msgRef);
    }

    private async Task HandleGPTStreamOutput(string input, MessageReference msgRef) {
        IUserMessage? firstMessage = null;
        await foreach (string output in _openAIHandler.GetGPTStreamOutputAsync(input)) {
            if (firstMessage?.Content.Length >= BotConfig._.IOMaxLength) {
                await ReplyAsync(GetIOMaxLenInfo(), messageReference: msgRef);
                return;
            }
            if (firstMessage != null) {
                await firstMessage.ModifyAsync(m => m.Content = firstMessage + output);
            } else {
                firstMessage = await ReplyAsync(output, messageReference: msgRef);
            }
        }
    }
}
