using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.IO;

namespace DrVegapunk.Bot.Web.Handlers;

public static class WebHostManager {
    public static IHostBuilder CreateBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureHostConfiguration(configHost => {
                configHost.SetBasePath(Directory.GetCurrentDirectory());
                configHost.AddJsonFile("Config/hostsettings.json", optional: true);
                configHost.AddEnvironmentVariables(prefix: "BOT_");
                configHost.AddCommandLine(args);
            })
            .ConfigureAppConfiguration((hostContext, configApp) => {
                configApp.AddJsonFile("Config/appsettings.json", optional: true);
                configApp.AddJsonFile(
                    $"Config/appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json",
                    optional: true);
                configApp.AddEnvironmentVariables(prefix: "BOT_");
                configApp.AddCommandLine(args);
            })
            .ConfigureLogging((hostContext, configLogging) => {
                configLogging.AddConfiguration(hostContext.Configuration.GetSection("Logging"));
                configLogging.AddConsole();
                configLogging.AddDebug();
            })
            .ConfigureWebHostDefaults(webBuilder => {
                webBuilder.UseStartup<Startup>().ConfigureLogging(logging => {
                    logging.AddConsole();
                    logging.AddDebug();
                });
            })
            .UseConsoleLifetime();
}
