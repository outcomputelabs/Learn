using Learn.Server.Grains;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Hosting;
using Serilog;
using System;
using System.Threading.Tasks;

namespace Learn.Server.Silo
{
    public static class Program
    {
        private static async Task Main()
        {
            using var host = Host
                .CreateDefaultBuilder()
                .ConfigureLogging(logging =>
                {
                    var logger = new LoggerConfiguration()
                        .WriteTo.Console()
                        .CreateLogger();

                    logging.ClearProviders();
                    logging.AddSerilog(logger, true);
                })
                .ConfigureServices((context, services) =>
                {
                    services.AddRandomGenerator();
                    services.AddSqlServerRepository(options =>
                    {
                        options.ConnectionString = context.Configuration.GetConnectionString("Learn");
                    });
                })
                .UseOrleans(orleans =>
                {
                    orleans.UseLocalhostClustering();
                    orleans.ConfigureApplicationParts(manager =>
                    {
                        manager.AddApplicationPart(typeof(WeatherForecastGrain).Assembly).WithReferences();
                    });
                })
                .UseConsoleLifetime()
                .Build();

            try
            {
                await host.RunAsync().ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                /* noop */
            }
        }
    }
}