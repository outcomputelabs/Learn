using AutoMapper;
using Learn.Server.Grains;
using Learn.WebApp.Shared;
using Microsoft.AspNetCore.Mvc;
using Orleans;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Learn.WebApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IGrainFactory _factory;
        private readonly IMapper _mapper;

        public WeatherForecastController(IGrainFactory factory, IMapper mapper)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WeatherForecastModel>>> GetAsync()
        {
            var forecasts = await _factory
                .GetWeatherForecastGrain()
                .GetAllAsync()
                .ConfigureAwait(false);

            var result = _mapper.Map<IEnumerable<WeatherForecastModel>>(forecasts);

            return Ok(result);
        }
    }
}