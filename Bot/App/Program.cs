using Microsoft.Extensions.DependencyInjection;

namespace DrVegapunk.Bot.App;

public class Program {
    private readonly IServiceProvider _services;
    private readonly MainStart _mainStart;

    static Task Main() => new Program().MainAsync();

    private Program() {
        _services = ServiceInstaller.InstallServices();
        _mainStart = _services.GetRequiredService<MainStart>();
    }

    private async Task MainAsync() {
        await _mainStart.StartAsync(_services);

        // Wait infinitely so the bot actually stays connected.
        await Task.Delay(Timeout.Infinite);
    }

}
