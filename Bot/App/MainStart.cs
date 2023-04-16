using Discord;
using Discord.WebSocket;
using DrVegapunk.Bot.App.Handlers;

namespace DrVegapunk.Bot.App;

public class MainStart {
    private readonly CommandHandler _commandHandler;
    private readonly DiscordSocketClient _client;

    public MainStart(CommandHandler commandHandler, 
                     DiscordSocketClient client) {
        _commandHandler = commandHandler;
        _client = client;
    }

    public async Task StartAsync(IServiceProvider services) {
        await _commandHandler.InitCommands(services);

        await _client.LoginAsync(
            TokenType.Bot,
            Environment.GetEnvironmentVariable("DISCORD_GPT_BOT_TOKEN")
        );

        await _client.StartAsync();
    }
}
