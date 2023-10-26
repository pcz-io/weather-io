using Microsoft.AspNetCore.Mvc;
using WeatherIO.Common.Data.Models;
using WeatherIO.Server.Data.Interfaces;

namespace WeatherIO.Server.Data.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherForecastService _forecastService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecastService weatherForecastService)
        {
            _logger = logger;
            _forecastService = weatherForecastService;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            _logger.LogInformation("Executed GET on WeatherForecastController");
            return _forecastService.Get();
        }
    }
}
