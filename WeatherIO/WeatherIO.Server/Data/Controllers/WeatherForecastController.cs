using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;
using WeatherIO.Common.Data.Models.APIResponses;
using WeatherIO.Server.Data.Services;

namespace WeatherIO.Server.Data.Controllers
{
	/// <summary>
	/// This class is responsible for handling forecase endpoints such as
	/// `get-forecast-by-city` and `get-forecast`
	/// </summary>
	[ApiController]
    [Route("api")]
    public class WeatherForecastController : Controller
	{
		/// <summary>
		/// Dependency injected ForecastService service
		/// </summary>
		private readonly ForecastService _forecastService;

		/// <summary>
		/// Dependency injected GeocodingService service
		/// </summary>
		private readonly GeocodingService _geocodingService;

		/// <summary>
		/// Constructor with dependency injected ForecastService, GeocodingService services
		/// </summary>
		/// <param name="forecastService"></param>
		/// <param name="geocodingService"></param>
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

		/// <summary>
		/// This method handles get-forecast-by-city.
		/// </summary>
		/// <param name="name">Name of the city</param>
		/// <param name="timezone">Optional timezone parameter. The default value is "Europe/Warsaw"</param>
		/// <param name="model">
        /// Weather forecast model to use.
        /// Value could be "dwdicon" or "ecmwf".
        /// This is a optional parameter and the default value is "dwdicon"
        /// </param>
		/// <returns></returns>
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

		/// <summary>
		/// This method handles get-forecast.
		/// </summary>
		/// <param name="latitude">Latidtude of the location</param>
		/// <param name="longitude">Longitude of the location</param>
		/// <param name="timezone">Optional timezone parameter. The default value is "Europe/Warsaw"</param>
		/// <param name="model">
		/// Weather forecast model to use.
		/// Value could be "dwdicon" or "ecmwf".
		/// This is a optional parameter and the default value is "dwdicon"
		/// </param>
		/// <returns>
        /// Status 200 Ok with ForecastResponse as body.
        /// Otherwise 404 NotFound with Error.
        /// </returns>
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