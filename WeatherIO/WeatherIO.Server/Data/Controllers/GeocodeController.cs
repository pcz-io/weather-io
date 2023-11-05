using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WeatherIO.Common.Data.Models.APIResponses;
using WeatherIO.Server.Data.Services;

namespace WeatherIO.Server.Data.Controllers
{
    [ApiController]
    [Route("api")]
    public class GeocodeController : Controller
    {
        private readonly GeocodingService _geocodingService;

        public GeocodeController(GeocodingService geocodingService)
        {
            _geocodingService = geocodingService;
        }

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
