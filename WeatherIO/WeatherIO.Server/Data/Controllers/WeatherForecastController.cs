using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;
using WeatherIO.Common.Data.Models.APIResponses;
using WeatherIO.Server.Data.Services;

namespace WeatherIO.Server.Data.Controllers
{
    [ApiController]
    [Route("api")]
    public class WeatherForecastController : Controller
    {
        private readonly ForecastService _forecastService;
        private readonly GeocodingService _geocodingService;

        public WeatherForecastController(ForecastService forecastService, GeocodingService geocodingService)
        {
            _forecastService = forecastService;
            _geocodingService = geocodingService;
        }

        [HttpGet]
        [Route("get-forecast-by-city")]
        public async Task<IActionResult> GetForecastByCity(string name, string timezone = "Europe/Warsaw")
        {
            var geo = await _geocodingService.GetGeocodingModelByNameAsync(name);
            if (geo == null)
            {
                return NotFound(new Error
                {
                    Message = $"Nie znaleziono miasta o nazwie {name}"
                });
            }
            var forecast = await _forecastService.GetForecastAsync(geo[0].Latitude, geo[0].Longitude, timezone);
            if (forecast == null)
            {
                return NotFound(new Error
                {
                    Message = "Nie znaleziono prognozy dla podanej lokalizacji"
                });
            }
            return Json(forecast.ToResponse());
        }

        [HttpGet]
        [Route("get-forecast")]
        public async Task<IActionResult> GetForecast(double latitude, double longitude, string timezone = "Europe/Warsaw")
        {
            var forecast = await _forecastService.GetForecastAsync(latitude, longitude, timezone);
            if (forecast == null)
            {
                return NotFound(new Error
                {
                    Message = "Nie znaleziono prognozy dla podanej lokalizacji"
                });
            }
            return Json(forecast.ToResponse());
        }
    }
}