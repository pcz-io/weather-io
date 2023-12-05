using System.Net;
using System.Text.Json;
using WeatherIO.Server.Data.Models.OpenMeteo;

namespace WeatherIO.Server.Data.Services
{
    /// <summary>
    /// This service is responsible for communication with OpenMeteo Geocoding API
    /// </summary>
    public class GeocodingService
    {
        /// <summary>
        /// Dependency injected http client factory used to make http requests
        /// </summary>
        private readonly IHttpClientFactory _httpClientFactory;

        /// <summary>
        /// Constructor with injected IHttpClientFactory
        /// </summary>
        /// <param name="httpClientFactory"></param>
        public GeocodingService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// Gets list of geocoding data for given city name
        /// </summary>
        /// <param name="name">City name query</param>
        /// <returns>List of the GeocodingResult</returns>
        public async Task<IList<GeocodingModel.GeocodingResult>?> GetGeocodingModelByNameAsync(string name)
        {
            var url = $"https://geocoding-api.open-meteo.com/v1/search?name={name}&count=10&language=pl&format=json";
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync(url);

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            var geocodingData = JsonSerializer.Deserialize<GeocodingModel>(response.Content.ReadAsStream());
            var results = geocodingData?.Results;

            if (results == null)
                return null;
            return results;
        }

        /// <summary>
        /// Gets geocoding data for given city name
        /// </summary>
        /// <param name="id">Id of the city</param>
        /// <returns>GeocodingResult</returns>
        public async Task<GeocodingModel.GeocodingResult?> GetGeocodingModelByIdAsync(int id)
        {
            var url = $"https://geocoding-api.open-meteo.com/v1/get?id={id}";
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync(url);

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            var result = JsonSerializer.Deserialize<GeocodingModel.GeocodingResult>(response.Content.ReadAsStream());

            if (result == null)
                return null;
            return result;
        }
    }
}
