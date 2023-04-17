using Discord;
using Discord.Commands;
using DrVegapunk.Bot.App;
using DrVegapunk.Bot.App.Managers;
using System.Threading.Tasks;

namespace DrVegapunk.Bot.Modules {
    public class DallEModule : OpenAIModule {
        protected override string ReachedMaxAttemptsMsg =>
            $"você atingiu o limite de uso diário de {BotConfig._.MaxUserAttemptsToDallE} " +
            "usos da funcionalidade de geração de imagens do Dr. Vegapunk. " +
            "Tente novamente após 24 horas ou pague um adicional de " +
            $"US${0.02 * BotConfig._.MaxUserAttemptsToDallE} para o Natan.";

        public DallEModule(OpenAIHandler openAiHandler) : base(
            openAiHandler, 
            BotConfig._.MaxUserAttemptsToDallE
        ) { }

        [Command("imagine")]
        [Summary("Requests an image from DallE")]
        public async Task ReplyDellEOutput([Remainder] string input) {
            var msgRef = new MessageReference(Context.Message.Id);

            if (!await IsInputValidAsync(input, msgRef)) return;

            var img = await _openAIHandler.GetDallEOutputAsync(input);

            await ReplyAsync(img.Data[0].Url, messageReference: msgRef);
        }
    }
}
