using Discord.Commands;
using Discord.WebSocket;
using DrVegapunk.Bot.App.Utils;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace DrVegapunk.Bot.App.Handlers;

public class CommandHandler {
    private static readonly CommandServiceConfig _commandSvcConfig = new() { DefaultRunMode = RunMode.Async };
    private readonly CommandService _commandSvc = new(_commandSvcConfig);
    private readonly DiscordSocketClient _client;
    private readonly BotConfig _botConfig = BotConfig._;
    IServiceProvider? _services;

    public CommandHandler(DiscordSocketClient client) {
        _client = client;
        LogManager.RegisterLog(_client);
        LogManager.RegisterLog(_commandSvc);
    }

    public async Task InitCommands(IServiceProvider services) {
        _services = services;
        await _commandSvc.AddModulesAsync(Assembly.GetEntryAssembly(), services);
        _client.MessageReceived += HandleCommandAsync;
    }

    private async Task HandleCommandAsync(SocketMessage sktMsg) {
        if (sktMsg is not SocketUserMessage msg ||
            msg.Author.IsBot ||
            msg.Author.Id == _client.CurrentUser.Id
        ) return;

        int commandStartIndex = 0;

        if (!msg.HasCharPrefix(_botConfig.CommandPrefix, ref commandStartIndex) &&
            !msg.HasCharPrefix(_botConfig.AlternativeCommandPrefix, ref commandStartIndex) &&
            !msg.HasMentionPrefix(_client.CurrentUser, ref commandStartIndex)) return;

        var context = new SocketCommandContext(_client, msg);

        var result = await _commandSvc.ExecuteAsync(context, commandStartIndex, _services);

        if (!result.IsSuccess && result.Error != CommandError.UnknownCommand)
            await msg.Channel.SendMessageAsync(result.ErrorReason);
    }
}
