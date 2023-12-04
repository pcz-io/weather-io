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

        /// <summary>
        /// Maps string to ForecastService.Model
        ///   "dwdicon" -> ForecastService.Model.DWDIcon
        ///   "ecmwf"   -> ForecastService.Model.ECMWF
        /// </summary>
        /// <param name="model">Input string</param>
        /// <returns>ForecastService.Model</returns>
        private ForecastService.Model StringToForecastModel(string? model)
        {
            return model switch
            {
                "dwdicon" => ForecastService.Model.DWDIcon,
                "ecmwf" => ForecastService.Model.ECMWF,
                _ => ForecastService.Model.DWDIcon
            };
        }

        [HttpGet]
        [Route("get-forecast-by-city")]
        public async Task<IActionResult> GetForecastByCity(string name, string? timezone = "Europe/Warsaw", string? model = "dwdicon")
        {
            var geo = await _geocodingService.GetGeocodingModelByNameAsync(name);
            if (geo == null)
            {
                return NotFound(new Error
                {
                    Message = $"Nie znaleziono miasta o nazwie {name}"
                });
            }

            var forecast = await _forecastService.GetForecastAsync(geo[0].Latitude, geo[0].Longitude, timezone!, StringToForecastModel(model));
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
        public async Task<IActionResult> GetForecast(double latitude, double longitude, string? timezone = "Europe/Warsaw", string? model = "dwdicon")
        {
            var forecast = await _forecastService.GetForecastAsync(latitude, longitude, timezone!, StringToForecastModel(model));
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