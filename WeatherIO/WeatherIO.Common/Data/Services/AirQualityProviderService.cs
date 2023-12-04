using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Web;
using WeatherIO.Common.Data.Interfaces;
using WeatherIO.Common.Data.Models.APIResponses;

namespace WeatherIO.Common.Data.Services
{
    public class AirQualityProviderService : IAirQualityProviderService
    {
        private readonly IConfigurationProviderService _configurationProviderService;
        private readonly IHttpClientProviderService _httpClientProviderService;

        public AirQualityProviderService(IConfigurationProviderService configurationProviderService, IHttpClientProviderService httpClientProviderService)
        {
            _configurationProviderService = configurationProviderService;
            _httpClientProviderService = httpClientProviderService;
        }

        public async Task<AirQualityResponse?> GetAirQualityAsync(double latitude, double longitude, string timezone = "Europe/Warsaw")
        {
            AirQualityResponse airQualityResponse;
            string timezoneEncoded = HttpUtility.UrlEncode(timezone);
            string latitudeEncoded = latitude.ToString(CultureInfo.InvariantCulture);
            string longitudeEncoded = longitude.ToString(CultureInfo.InvariantCulture);

            var url = $"{_configurationProviderService.Configuration.ServerAddress}/api/air-quality?latitude={latitudeEncoded}&longitude={longitudeEncoded}&timezone={timezoneEncoded}";

            var response = await _httpClientProviderService.GetAsync(url);

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            airQualityResponse = JsonSerializer.Deserialize<AirQualityResponse>(response.Content.ReadAsStream(), options)!;

            return airQualityResponse;
        }
    }
}
