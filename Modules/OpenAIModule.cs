using Discord.Commands;
using DrVegapunk.GPT.App;
using DrVegapunk.GPT.App.Managers;

namespace DrVegapunk.GPT.Modules {
    [Group("v")]
    public abstract class OpenAIModule : ModuleBase<SocketCommandContext> {
        protected static readonly int _inputMaxLen = BotConfig._.IOMaxLength;
        protected readonly OpenAIHandler _openAIHandler;

        protected OpenAIModule(OpenAIHandler openAiHandler) { _openAIHandler = openAiHandler; }

        protected static string GetIOMaxLenInfo(bool isInput = false) =>
            $"Se você deseja {(isInput ? "me perguntar" : "que eu responda")} " +
            $"algo com mais de {_inputMaxLen} caracteres, paga a mensalidade " +
            $"da API da OpenAI pra mim por favor (100 real por mês :D).";

        protected async Task<bool> IsInputValidAsync(string input) {
            if (string.IsNullOrWhiteSpace(input)) return false;

            await Context.Channel.TriggerTypingAsync();

            if (input.Length >= _inputMaxLen) {
                await ReplyAsync(GetIOMaxLenInfo(true));
                return false;
            }
            return true;
        }
    }
}
