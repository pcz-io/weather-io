using System.Globalization;
using System.Net;
using System.Text.Json;
using System.Web;
using WeatherIO.Server.Data.Models.OpenMeteo;

namespace WeatherIO.Server.Data.Services
{
    public class AirQualityService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AirQualityService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<AirQualityModel?> GetAirQualityAsync(double latitude, double longitude, string timezone)
        {
            var encodedTimezone = HttpUtility.UrlEncode(timezone);
            var latitudeEncoded = latitude.ToString(CultureInfo.InvariantCulture);
            var longitudeEncoded = longitude.ToString(CultureInfo.InvariantCulture);
            var url = $"https://air-quality-api.open-meteo.com/v1/air-quality?latitude={latitudeEncoded}&longitude={longitudeEncoded}&current=european_aqi,pm10,pm2_5,carbon_monoxide,nitrogen_dioxide,sulphur_dioxide,ozone&timezone={encodedTimezone}";

            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync(url);
            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonSerializer.Deserialize<AirQualityModel>(response.Content.ReadAsStream());
        }
    }
}
