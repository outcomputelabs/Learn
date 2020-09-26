using Learn.Server.Shared;
using Orleans;
using System;
using System.Collections.Immutable;
using System.Threading.Tasks;

namespace Learn.Server.Grains
{
    public interface IWeatherForecastGrain : IGrainWithGuidKey
    {
        public Task<ImmutableList<WeatherForecast>> GetAllAsync();
    }

    public static class WeatherForecastGrainExtensions
    {
        public static IWeatherForecastGrain GetWeatherForecastGrain(this IGrainFactory factory)
        {
            if (factory is null) throw new ArgumentNullException(nameof(factory));

            return factory.GetGrain<IWeatherForecastGrain>(Guid.Empty);
        }
    }
}