using System.Net;
using System.Text.Json;
using WeatherIO.Server.Data.Models.OpenMeteo;

namespace WeatherIO.Server.Data.Services
{
    public class GeocodingService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public GeocodingService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

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
