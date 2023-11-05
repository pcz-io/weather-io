using Microsoft.AspNetCore.Mvc;
using WeatherIO.Common.Data.Models.APIResponses;
using WeatherIO.Server.Data.Services;

namespace WeatherIO.Server.Data.Controllers
{
    [Route("api")]
    public class AirQualityController : Controller
    {
        private readonly AirQualityService _airQualityService;

        public AirQualityController(AirQualityService airQualityService)
        {
            _airQualityService = airQualityService;
        }

        [HttpGet]
        [Route("air-quality")]
        public async Task<IActionResult> AirQuality(double latitude, double longitude, string timezone = "Europe/Warsaw")
        {
            var airQuality = await _airQualityService.GetAirQualityAsync(latitude, longitude, timezone);

            if (airQuality == null)
            {
                return NotFound();
            }

            return Json(new AirQualityResponse
            {
                AirQualityIndex = airQuality.current.european_aqi,
                AirQualityString = airQuality.current.european_aqi switch
                {
                    >= 0 and < 20 => "dobra",
                    >= 20 and < 40 => "w miare",
                    >= 40 and < 60 => "średnia",
                    >= 60 and < 80 => "słaba",
                    >= 80 and < 100 => "bardzo słaba",
                    > 100 => "rakotwórcza",

                    _ => "nieznane"
                }
            });
        }
    }
}
