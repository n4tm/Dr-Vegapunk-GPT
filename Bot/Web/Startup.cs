using Discord.WebSocket;
using DrVegapunk.Bot.App;
using DrVegapunk.Bot.Modules;
using DrVegapunk.Bot.App.Handlers;
using DrVegapunk.Bot.App.Managers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading;

namespace DrVegapunk.Bot.Web;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration config)
    {
        Configuration = config;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        var clientConfig = new DiscordSocketConfig()
        {
            GatewayIntents =
                Discord.GatewayIntents.MessageContent |
                Discord.GatewayIntents.Guilds |
                Discord.GatewayIntents.GuildMessages |
                Discord.GatewayIntents.GuildMessageTyping
        };

        services.AddSingleton<DiscordSocketClient>(s => new(clientConfig))
                .AddSingleton<CommandHandler>()
                .AddSingleton<OpenAIHandler>()
                .AddSingleton<BotStarter>()
                .AddSingleton<HostedConsoleService>()

.AddSingleton<ChatGPTModule>()

.AddSingleton<DallEModule>()
                .AddControllersWithViews();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        // app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
        app.ApplicationServices.GetService<HostedConsoleService>()!
           .StartAsync(new CancellationToken())
           .Wait();
    }
}
