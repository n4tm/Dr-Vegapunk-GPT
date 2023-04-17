namespace DrVegapunk.Bot.Web {
    public class HostedConsoleService : IHostedService {
        private readonly ILogger _logger;
        private readonly IHostApplicationLifetime _appLifetime;

        public HostedConsoleService(
            ILogger<HostedConsoleService> logger,
            IHostApplicationLifetime appLifetime) {
            _logger = logger;
            _appLifetime = appLifetime;
        }

        public Task StartAsync(CancellationToken cancellationToken) {
            _appLifetime.ApplicationStarted.Register(OnStarted);
            _appLifetime.ApplicationStopping.Register(OnStopping);
            _appLifetime.ApplicationStopped.Register(OnStopped);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken) =>
            Task.CompletedTask;        

        private void OnStarted() {
            _logger.LogInformation("OnStarted has been called.");

            // Perform on-started activities here

            Pinger();
        }

        private void OnStopping() {
            _logger.LogInformation("OnStopping has been called.");

            // Perform on-stopping activities here
        }

        private void OnStopped() {
            _logger.LogInformation("OnStopped has been called.");

            // Perform post-stopped activities here
        }

        private async void Pinger() {
            _logger.LogInformation("Pinger Function Started");

            for (var i = 0; i < 100; i++) {
                _logger.LogInformation("Ping number: {i}", i);
                await Task.Delay(5000);
            }

            _logger.LogInformation("Pinger Function Finished");
        }
    }
}
