using Discord;
using Discord.Commands;
using DrVegapunk.GPT.App.Managers;

namespace DrVegapunk.GPT.Modules {
    public class DallEModule : OpenAIModule {
        public DallEModule(OpenAIHandler openAiHandler) : base(openAiHandler) { }

        [Command("imagine")]
        [Summary("Requests an image from DallE")]
        public async Task ReplyDellEOutput([Remainder] string input) {
            if (!await IsInputValidAsync(input)) return;

            var img = await _openAIHandler.GetDallEOutputAsync(input);

            var msgRef = new MessageReference(Context.Message.Id);

            await ReplyAsync(img.Data[0].Url, messageReference: msgRef);
        }
    }
}
