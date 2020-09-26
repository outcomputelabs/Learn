using Learn.Server.Grains;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Orleans;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Learn.WebApp.Server.Orleans
{
    internal sealed class LearnClusterClientHostedService : IHostedService, IAsyncDisposable
    {
        private readonly ILogger _logger;
        private readonly LearnClusterClientHostedServiceOptions _options;
        private readonly IClusterClient _client;

        public LearnClusterClientHostedService(ILogger<LearnClusterClientHostedService> logger, IOptions<LearnClusterClientHostedServiceOptions> options)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _options = options?.Value ?? throw new ArgumentNullException(nameof(options));

            _client = new ClientBuilder()
                .UseLocalhostClustering()
                .ConfigureApplicationParts(manager =>
                {
                    manager.AddApplicationPart(typeof(IWeatherForecastGrain).Assembly).WithReferences();
                })
                .Build();
        }

        private static string ServiceName => nameof(LearnClusterClientHostedService);

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.ServiceStarting(ServiceName);

            var attempt = 0;

            await _client
                .Connect(ex =>
                {
                    if (++attempt < _options.MaxConnectionAttempts)
                    {
                        _logger.FailedToConnectWillRetry(ServiceName, attempt, _options.MaxConnectionAttempts);

                        return Task.FromResult(true);
                    }
                    else
                    {
                        _logger.FailedToConnectWillNotRetry(ServiceName, attempt, _options.MaxConnectionAttempts);

                        return Task.FromResult(false);
                    }
                })
                .ConfigureAwait(false);

            _logger.ServiceStarted(ServiceName);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _client.Close().ConfigureAwait(false);

            _logger.ServiceStopped(ServiceName);
        }

        public IGrainFactory GrainFactory => _client;

        #region IAsyncDisposable

        public ValueTask DisposeAsync()
        {
            return _client.DisposeAsync();
        }

        #endregion IAsyncDisposable
    }
}