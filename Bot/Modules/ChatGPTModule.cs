using Discord;
using Discord.Commands;
using DrVegapunk.Bot.App;
using DrVegapunk.Bot.App.Managers;
using DrVegapunk.Bot.Modules;
using System.Threading.Tasks;

namespace DrVegapunk.Bot.Modules;

public class ChatGPTModule : OpenAIModule {
    protected override string ReachedMaxAttemptsMsg => 
        $"você atingiu o limite de uso diário de {BotConfig._.MaxUserAttemptsToChatGPT} " +
        "usos da funcionalidade de geração de texto do Dr. Vegapunk. " +
        "Tente novamente após 24 horas ou pague um adicional de " +
        $"US${0.002 * BotConfig._.MaxUserAttemptsToChatGPT} para o Natan.";

    public ChatGPTModule(OpenAIHandler openAiHandler) : base(
        openAiHandler, 
        BotConfig._.MaxUserAttemptsToChatGPT
    ) { }

    [Command("say")]
    [Summary("Requests an answer from ChatGPT")]
    public async Task ReplyChatGPTOutput([Remainder] string input) {
        var msgRef = new MessageReference(Context.Message.Id);

        if (!await IsInputValidAsync(input, msgRef)) return;

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
