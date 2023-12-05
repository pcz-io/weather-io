using Microsoft.AspNetCore.Mvc;
using WeatherIO.Common.Data.Models.APIResponses;
using WeatherIO.Server.Data.Services;

namespace WeatherIO.Server.Data.Controllers
{
    /// <summary>
    /// This class is responsible for handling air quality endpoint
    /// </summary>
    [Route("api")]
    public class AirQualityController : Controller
    {
        /// <summary>
        /// Dependency injected instance of a AirQualityService
        /// </summary>
        private readonly AirQualityService _airQualityService;

        /// <summary>
        /// Constructor with dependency injected service
        /// </summary>
        /// <param name="airQualityService"></param>
        public AirQualityController(AirQualityService airQualityService)
        {
            _airQualityService = airQualityService;
        }

        /// <summary>
        /// Function that handles air-quality API endpoint
        /// </summary>
        /// <param name="latitude">Latitude of the location</param>
        /// <param name="longitude">Longitude of the location</param>
        /// <param name="timezone">Timezone, the default is "Europe/Warsaw"</param>
        /// <returns>
        /// Response 200 Ok with AirQualityResponse as response body if no errors occurred.
        /// Otherwise response with status 404 NotFound
        /// </returns>
        [HttpGet]
        [Route("air-quality")]
        public async Task<IActionResult> AirQuality(double latitude, double longitude, string? timezone = "Europe/Warsaw")
        {
            var airQuality = await _airQualityService.GetAirQualityAsync(latitude, longitude, timezone!);

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
