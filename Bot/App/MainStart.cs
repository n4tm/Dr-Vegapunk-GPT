using Discord;
using Discord.WebSocket;
using DrVegapunk.Bot.App.Handlers;
using DrVegapunk.Bot.Web.Handlers;

namespace DrVegapunk.Bot.App;

public class MainStart {
    private readonly WebHostHandler _webHost;
    private readonly CommandHandler _commandHandler;
    private readonly DiscordSocketClient _client;

    public MainStart(WebHostHandler webHost,
                     CommandHandler commandHandler, 
                     DiscordSocketClient client) {
        _commandHandler = commandHandler;
        _client = client;
        _webHost = webHost;
    }

    public async Task StartAsync(IServiceProvider services, string[] args) {
        await _webHost.CreateHostBuilder(args).Build().RunAsync();

        await _commandHandler.InitCommands(services);

        await _client.LoginAsync(
            TokenType.Bot,
            Environment.GetEnvironmentVariable("DISCORD_GPT_BOT_TOKEN")
        );

        await _client.StartAsync();
    }
}
