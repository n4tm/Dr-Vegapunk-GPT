using Microsoft.Extensions.DependencyInjection;

namespace DrVegapunk.Bot.App;

public class Program {
    private readonly IServiceProvider _services;
    private readonly MainStart _mainStart;

    public static Task Main(string[] args) => 
        new Program().MainAsync(args);

    private Program() {
        _services = ServiceInstaller.InstallServices();
        _mainStart = _services.GetRequiredService<MainStart>();
    }

    private async Task MainAsync(string[] args) {
        await _mainStart.StartAsync(_services, args);

        // Wait infinitely so the bot actually stays connected.
        await Task.Delay(Timeout.Infinite);
    }
}
