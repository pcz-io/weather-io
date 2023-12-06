using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Web;
using WeatherIO.Common.Data.Interfaces;
using WeatherIO.Common.Data.Models.APIResponses;

namespace WeatherIO.Common.Data.Services
{
    /// <summary>
    /// This is a service that provides air quality data from external API.
    /// </summary>
    public class AirQualityProviderService : IAirQualityProviderService
    {
        /// <summary>
        /// Configuration provider service.
        /// </summary>
        private readonly IConfigurationProviderService _configurationProviderService;
        /// <summary>
        /// Http client provider service.
        /// </summary>
        private readonly IHttpClientProviderService _httpClientProviderService;

        /// <summary>
        /// Constructor of the service.
        /// </summary>
        /// <param name="configurationProviderService"></param>
        /// <param name="httpClientProviderService"></param>
        public AirQualityProviderService(IConfigurationProviderService configurationProviderService, IHttpClientProviderService httpClientProviderService)
        {
            _configurationProviderService = configurationProviderService;
            _httpClientProviderService = httpClientProviderService;
        }

        /// <summary>
        /// This method gets air quality data from external API.
        /// </summary>
        /// <param name="latitude"> Latitude of the location. </param>
        /// <param name="longitude"> Longitude of the location. </param>
        /// <param name="timezone"> Timezone of the location. </param>
        /// <returns></returns>
        public async Task<AirQualityResponse?> GetAirQualityAsync(double latitude, double longitude, string timezone = "Europe/Warsaw")
        {
            AirQualityResponse airQualityResponse;
            string timezoneEncoded = HttpUtility.UrlEncode(timezone);
            string latitudeEncoded = latitude.ToString(CultureInfo.InvariantCulture);
            string longitudeEncoded = longitude.ToString(CultureInfo.InvariantCulture);

            var url = $"{_configurationProviderService.Configuration.ServerAddress}/api/air-quality?latitude={latitudeEncoded}&longitude={longitudeEncoded}&timezone={timezoneEncoded}";

            var response = await _httpClientProviderService.HttpClient.GetAsync(url);

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
