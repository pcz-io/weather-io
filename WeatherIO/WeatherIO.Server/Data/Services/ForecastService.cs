using System.Globalization;
using System.Net;
using System.Text.Json;
using System.Web;
using WeatherIO.Server.Data.Models.OpenMeteo;

namespace WeatherIO.Server.Data.Services
{
    public class ForecastService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ForecastService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ForecastModel?> GetForecastAsync(double latitude, double longitude, string timezone)
        {
            var encodedTimezone = HttpUtility.UrlEncode(timezone);
            var latitudeEncoded = latitude.ToString(CultureInfo.InvariantCulture);
            var longitudeEncoded = longitude.ToString(CultureInfo.InvariantCulture);
            var url = $"https://api.open-meteo.com/v1/dwd-icon?latitude={latitudeEncoded}&longitude={longitudeEncoded}&current=temperature_2m,relativehumidity_2m,apparent_temperature,precipitation,rain,showers,snowfall,weathercode,cloudcover,surface_pressure,windspeed_10m,winddirection_10m,windgusts_10m&hourly=temperature_2m,relativehumidity_2m,dewpoint_2m,apparent_temperature,precipitation,rain,showers,snowfall,weathercode,surface_pressure,cloudcover,cloudcover_low,cloudcover_mid,cloudcover_high,windspeed_10m,winddirection_10m,windgusts_10m&daily=sunrise,sunset&timezone={encodedTimezone}";

            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync(url);
            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonSerializer.Deserialize<ForecastModel>(response.Content.ReadAsStream());
        }
    }
}
