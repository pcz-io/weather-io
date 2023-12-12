using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WeatherIO.Common.Data.Models.APIResponses;
using WeatherIO.Server.Data.Services;

namespace WeatherIO.Server.Data.Controllers
{
    /// <summary>
    /// This class is responsible for handling geocoding endpoints such as
    /// `geocode` and `geocode-by-id`
    /// </summary>
    [ApiController]
    [Route("api")]
    public class GeocodeController : Controller
    {
        /// <summary>
        /// Dependency injected GeocodingService service
        /// </summary>
        private readonly GeocodingService _geocodingService;

        /// <summary>
        /// Constructor with dependency injected GeocodingService serivce
        /// </summary>
        /// <param name="geocodingService"></param>
        public GeocodeController(GeocodingService geocodingService)
        {
            _geocodingService = geocodingService;
        }

        /// <summary>
        /// This function handles `geocode` endpoint.
        /// </summary>
        /// <param name="name">Name of the city for which find a longitude and latitude</param>
        /// <returns>
        /// Status 200 Ok with JSON array of GeocodeResponse as response body if no errors occurred.
        /// Otherwise 404 NotFound with Error
        /// </returns>
        [HttpGet]
        [Route("geocode")]
        public async Task<IActionResult> Geocode(string name)
        {
            var result = await _geocodingService.GetGeocodingModelByNameAsync(name);

            if (result == null)
            {
                return NotFound(new Error
                {
                    Message = $"Nie znaleziono miasta o podanej nazwie: {name}"
                });
            }

            return Json(result.Select(x => new GeocodeResponse
            {
                Id = x.Id,
                CityName = x.Name,
                Longitude = x.Longitude,
                Latitude = x.Latitude
            }));
        }

        /// <summary>
        /// This function handles `geocode-by-id` endpoint.
        /// </summary>
        /// <param name="id">Id of the city</param>
        /// <returns>
        /// Status 200 Ok with GeocodeResponse as response body if no errors occurred.
        /// Otherwise 404 NotFound with Error
        /// </returns>
        [HttpGet]
        [Route("geocode-by-id")]
        public async Task<IActionResult> GeocodeById(int id)
        {
            var result = await _geocodingService.GetGeocodingModelByIdAsync(id);

            if (result == null)
            {
                return NotFound(new Error
                {
                    Message = $"Nie znaleziono miasta o podanym id: {id}"
                });
            }

            return Json(new GeocodeResponse
            {
                Id = result.Id,
                CityName = result.Name,
                Longitude = result.Longitude,
                Latitude = result.Latitude
            });
        }
    }
}
