using Learn.WebApp.Server.Orleans;
using Microsoft.Extensions.Hosting;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    internal static class LearnClusterClientHostedServiceDependencyInjectionExtensions
    {
        public static IServiceCollection AddLearnClusterClient(this IServiceCollection services, Action<LearnClusterClientHostedServiceOptions> configure)
        {
            return services
                .AddSingleton<LearnClusterClientHostedService>()
                .AddSingleton<IHostedService>(provider => provider.GetRequiredService<LearnClusterClientHostedService>())
                .AddSingleton(provider => provider.GetService<LearnClusterClientHostedService>().GrainFactory)
                .AddOptions<LearnClusterClientHostedServiceOptions>()
                .Configure(configure)
                .ValidateDataAnnotations()
                .Services;
        }
    }
}