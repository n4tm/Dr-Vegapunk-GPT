using Discord;
using Discord.WebSocket;
using DrVegapunk.Bot.App.Handlers;
using System;
using System.Threading.Tasks;

namespace DrVegapunk.Bot.App;

public class BotStarter {
    private readonly CommandHandler _commandHandler;
    private readonly DiscordSocketClient _client;
    private readonly IServiceProvider _serviceProvider;

    public BotStarter(CommandHandler commandHandler, 
                      DiscordSocketClient client,
                      IServiceProvider serviceProvider) {
        _commandHandler = commandHandler;
        _client = client;
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync() {
        await _commandHandler.InitCommands(_serviceProvider);

        await _client.LoginAsync(
            TokenType.Bot,
            Environment.GetEnvironmentVariable("DISCORD_GPT_BOT_TOKEN")
        );

        await _client.StartAsync();
    }
}
