using Discord;
using Discord.Commands;
using DrVegapunk.Bot.App;
using DrVegapunk.Bot.App.Managers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DrVegapunk.Bot.Modules {
    [Group("v")]
    public abstract class OpenAIModule : ModuleBase<SocketCommandContext> {
        private static readonly int _inputMaxLen = BotConfig._.IOMaxLength;
        protected readonly OpenAIHandler _openAIHandler;
        private readonly int _defaultMaxAttempts;
        private readonly Dictionary<ulong, int> _userAttemptsById = new();

        protected abstract string ReachedMaxAttemptsMsg { get; }

        protected OpenAIModule(OpenAIHandler openAiHandler, int defaultMaxAttempts) { 
            _openAIHandler = openAiHandler; 
            _defaultMaxAttempts = defaultMaxAttempts;
        }

        protected static string GetIOMaxLenInfo(bool isInput = false) =>
            $"Se você deseja {(isInput ? "me perguntar" : "que eu responda")} " +
            $"algo com mais de {_inputMaxLen} caracteres, paga a mensalidade " +
            $"da API da OpenAI pra mim por favor (100 real por mês :D).";

        protected async Task<bool> IsInputValidAsync(string input, MessageReference msgRef) {
            if (string.IsNullOrWhiteSpace(input)) return false;

            await Context.Channel.TriggerTypingAsync();

            var user = Context.User;

            var userId = user.Id;

            if (!_userAttemptsById.TryGetValue(userId, out int userAttempts)) {
                _userAttemptsById.Add(userId, _defaultMaxAttempts);
            }

            if (userAttempts >= _defaultMaxAttempts) {
                await ReplyAsync(
                    $"{user.Mention}, {ReachedMaxAttemptsMsg}", 
                    messageReference: msgRef
                );
                return false;
            }

            _userAttemptsById[userId] += 1;

            if (_userAttemptsById[userId] == _defaultMaxAttempts - 1) {
                await ReplyAsync(
                    $"{user.Mention}, resta apenas mais 1 uso dessa funcionalidade para você!", 
                    messageReference: msgRef
                );
            }

            if (input.Length >= _inputMaxLen) {
                await ReplyAsync(GetIOMaxLenInfo(true), messageReference: msgRef);
                return false;
            }

            return true;
        }
    }
}
