using Learn.Core.RandomGenerators;
using Learn.Server.Shared;
using Orleans;
using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace Learn.Server.Grains
{
    public class WeatherForecastGrain : Grain, IWeatherForecastGrain
    {
        private readonly IRandomGenerator _random;

        public WeatherForecastGrain(IRandomGenerator random)
        {
            _random = random ?? throw new ArgumentNullException(nameof(random));
        }

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public Task<ImmutableList<WeatherForecast>> GetAllAsync()
        {
            return Enumerable
                .Range(1, 5)
                .Select(index => new WeatherForecast(
                    DateTime.Now.AddDays(index),
                    _random.Next(-20, 55),
                    Summaries[_random.Next(Summaries.Length)]))
                .ToImmutableList()
                .ToTask();
        }
    }
}