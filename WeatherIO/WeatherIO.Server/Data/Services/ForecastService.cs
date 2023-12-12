using System.Globalization;
using System.Net;
using System.Text.Json;
using System.Web;
using WeatherIO.Server.Data.Models.OpenMeteo;

namespace WeatherIO.Server.Data.Services
{
    /// <summary>
    /// This service is responsible for communication with OpenMeteo Forecast API
    /// </summary>
    public class ForecastService
    {
        /// <summary>
        /// Enum representation of the selected model
        /// </summary>
        public enum Model
        {
            DWDIcon,
            ECMWF
        }

        /// <summary>
        /// Dependency injected http client factory used to make http requests
        /// </summary>
        private readonly IHttpClientFactory _httpClientFactory;

        /// <summary>
        /// Constructor with injected IHttpClientFactory
        /// </summary>
        /// <param name="httpClientFactory"></param>
        public ForecastService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// Gets forecast for given location and forecast model
        /// </summary>
        /// <param name="latitude">Latitude of the location</param>
        /// <param name="longitude">Longitude of the location</param>
        /// <param name="timezone">Timezone</param>
        /// <param name="model">Forecast model to use</param>
        /// <returns></returns>
        public async Task<ForecastModel?> GetForecastAsync(double latitude, double longitude, string timezone, Model model = Model.DWDIcon)
        {
            var modelStr = model switch
            {
                Model.DWDIcon => "dwd-icon",
                Model.ECMWF => "ecmwf",
                _ => "dwd-icon"
            };
            var encodedTimezone = HttpUtility.UrlEncode(timezone);
            var latitudeEncoded = latitude.ToString(CultureInfo.InvariantCulture);
            var longitudeEncoded = longitude.ToString(CultureInfo.InvariantCulture);
            var url = $"https://api.open-meteo.com/v1/{modelStr}?latitude={latitudeEncoded}&longitude={longitudeEncoded}&current=temperature_2m,relative_humidity_2m,apparent_temperature,precipitation,rain,showers,snowfall,weathercode,cloudcover,surface_pressure,wind_speed_10m,wind_direction_10m,wind_gusts_10m&hourly=temperature_2m,relative_humidity_2m,dewpoint_2m,apparent_temperature,precipitation,rain,showers,snowfall,weathercode,surface_pressure,cloudcover,cloudcover_low,cloudcover_mid,cloudcover_high,wind_speed_10m,wind_direction_10m,wind_gusts_10m&daily=sunrise,sunset&timezone={encodedTimezone}";

            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync(url);
            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            if (model == Model.DWDIcon)
            {
                return JsonSerializer.Deserialize<ForecastModel>(response.Content.ReadAsStream());
            }
            else if (model == Model.ECMWF)
            {
                var forecast = JsonSerializer.Deserialize<ForecastModel>(response.Content.ReadAsStream());
                if (forecast == null)
                    return null;

                // No showers with ECMWF so zeroes everywhere
                forecast.hourly.showers = Enumerable.Repeat(0.0, forecast.hourly.time.Count).ToList();

                var now = DateTime.Now;
                int index = 0;
                for (int i = 0; i < forecast.hourly.time.Count; ++i)
                {
                    if(now < forecast.hourly.time[i])
                    {
                        index = Math.Clamp(i - 1, 0, int.MaxValue);
                        break;
                    }
                }

                T? ForwardNullElement<T>(IList<T>? value, int index) where T : struct
                    => value == null ? null : value[index];

                forecast.current = new()
                {
                    apparent_temperature = ForwardNullElement(forecast.hourly.apparent_temperature, index),
                    cloudcover = ForwardNullElement(forecast.hourly.cloudcover, index),
                    precipitation = ForwardNullElement(forecast.hourly.precipitation, index),
                    rain = ForwardNullElement(forecast.hourly.rain, index),
                    relative_humidity_2m = ForwardNullElement(forecast.hourly.relative_humidity_2m, index),
                    showers = ForwardNullElement(forecast.hourly.showers, index),
                    snowfall = ForwardNullElement(forecast.hourly.snowfall, index),
                    surface_pressure = ForwardNullElement(forecast.hourly.surface_pressure, index),
                    temperature_2m = ForwardNullElement(forecast.hourly.temperature_2m, index),
                    time = forecast.hourly.time[index],
                    weathercode = ForwardNullElement(forecast.hourly.weathercode, index),
                    wind_direction_10m = ForwardNullElement(forecast.hourly.wind_direction_10m, index),
                    wind_gusts_10m = ForwardNullElement(forecast.hourly.wind_gusts_10m, index),
                    wind_speed_10m = ForwardNullElement(forecast.hourly.wind_speed_10m, index)
                };
                return forecast;
            }
            return null;
        }
    }
}
