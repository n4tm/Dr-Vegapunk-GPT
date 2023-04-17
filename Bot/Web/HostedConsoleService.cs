using DrVegapunk.Bot.App;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace DrVegapunk.Bot.Web {
    public class HostedConsoleService : IHostedService {
        private readonly ILogger _logger;
        private readonly IHostApplicationLifetime _appLifetime;
        private readonly BotStarter _botStarter;

        public HostedConsoleService(
            ILogger<HostedConsoleService> logger,
            IHostApplicationLifetime appLifetime,
            BotStarter botStarter) {
            _logger = logger;
            _appLifetime = appLifetime;
            _botStarter = botStarter;
        }

        public async Task StartAsync(CancellationToken cancellationToken) {
            _appLifetime.ApplicationStarted.Register(OnStarted);
            _appLifetime.ApplicationStopping.Register(OnStopping);
            _appLifetime.ApplicationStopped.Register(OnStopped);

            await _botStarter.StartAsync();
        }

        public Task StopAsync(CancellationToken cancellationToken) =>
            Task.CompletedTask;        

        private void OnStarted() {
            _logger.LogInformation("OnStarted has been called.");

            // Perform on-started activities here
        }

        private void OnStopping() {
            _logger.LogInformation("OnStopping has been called.");

            // Perform on-stopping activities here
        }

        private void OnStopped() {
            _logger.LogInformation("OnStopped has been called.");

            // Perform post-stopped activities here
        }
    }
}
