using Orleans.Concurrency;
using System;

namespace Learn.Server.Shared
{
    [Immutable]
    public class WeatherForecast
    {
        public WeatherForecast(DateTime date, int temperatureC, string summary)
        {
            Date = date;
            TemperatureC = temperatureC;
            Summary = summary;
        }

        public DateTime Date { get; }

        public int TemperatureC { get; }

        public string Summary { get; }
    }
}