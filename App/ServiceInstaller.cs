﻿using Discord.WebSocket;
using DrVegapunk.GPT.App.Handlers;
using DrVegapunk.GPT.App.Managers;
using Microsoft.Extensions.DependencyInjection;

namespace DrVegapunk.GPT.App;

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

            // Should be the last one
            .AddSingleton<MainStart>()
            .BuildServiceProvider();
}