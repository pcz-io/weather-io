using System.Globalization;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using System.Web;
using WeatherIO.Server.Data.Models.OpenMeteo;
using static MudBlazor.Colors;

namespace WeatherIO.Server.Data.Services
{
    public class ForecastService
    {
        public enum Model
        {
            DWDIcon,
            ECMWF
        }

        private readonly IHttpClientFactory _httpClientFactory;

        public ForecastService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

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

                forecast.current = new()
                {
                    apparent_temperature = forecast.hourly.apparent_temperature[index],
                    cloudcover = forecast.hourly.cloudcover[index],
                    precipitation = forecast.hourly.precipitation[index],
                    rain = forecast.hourly.rain[index],
                    relative_humidity_2m = forecast.hourly.relative_humidity_2m[index],
                    showers = forecast.hourly.showers[index],
                    snowfall = forecast.hourly.snowfall[index],
                    surface_pressure = forecast.hourly.surface_pressure[index],
                    temperature_2m = forecast.hourly.temperature_2m[index],
                    time = forecast.hourly.time[index],
                    weathercode = forecast.hourly.weathercode[index],
                    wind_direction_10m = forecast.hourly.wind_direction_10m[index],
                    wind_gusts_10m = forecast.hourly.wind_gusts_10m[index],
                    wind_speed_10m = forecast.hourly.wind_speed_10m[index]
                };
                return forecast;
            }
            return null;
        }
    }
}
