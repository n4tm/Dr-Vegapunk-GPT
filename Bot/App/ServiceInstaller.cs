using Discord.WebSocket;
using DrVegapunk.Bot.App.Handlers;
using DrVegapunk.Bot.App.Managers;
using DrVegapunk.Bot.Web.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace DrVegapunk.Bot.App;

public static class ServiceInstaller {
    private static readonly DiscordSocketConfig _clientConfig = new() { 
        GatewayIntents =
            Discord.GatewayIntents.MessageContent |
            Discord.GatewayIntents.Guilds |
            Discord.GatewayIntents.GuildMessages |
            Discord.GatewayIntents.GuildMessageTyping
    };

    public static IServiceProvider InstallServices() => 
        new ServiceCollection()
            .AddSingleton<DiscordSocketClient>(s => new(_clientConfig))
            .AddSingleton<CommandHandler>()
            .AddSingleton<OpenAIHandler>()
            .AddSingleton<WebHostHandler>()

            // Should be the last one
            .AddSingleton<MainStart>()
            .BuildServiceProvider();
}