using System.Net;
using System.Text.Json;
using System.Web;
using WeatherIO.Common.Data.Interfaces;
using WeatherIO.Common.Data.Models.APIResponses;
using System.Globalization;

namespace WeatherIO.Common.Data.Services
{
	public class WeatherForecastProviderService : IWeatherForecastProviderService
	{
		private readonly IConfigurationProviderService _configurationProviderService;
		private readonly IHttpClientProviderService _httpClientProviderService;

		public WeatherForecastProviderService(IConfigurationProviderService configurationProviderService, IHttpClientProviderService httpClientProviderService)
		{
			_configurationProviderService = configurationProviderService;
			_httpClientProviderService = httpClientProviderService;
		}

		public async Task<ForecastResponse?> GetForecasAsync(double latitude, double longitude, string timezone = "Europe/Warsaw")
		{
			ForecastResponse forecastResponse;
			string timezoneEncoded = HttpUtility.UrlEncode(timezone);
			string latitudeEncoded = latitude.ToString(CultureInfo.InvariantCulture);
			string longitudeEncoded = longitude.ToString(CultureInfo.InvariantCulture);

			var url = $"{_configurationProviderService.Configuration.ServerAddress}/api/get-forecast?latitude={latitudeEncoded}&longitude={longitudeEncoded}&timezone={timezoneEncoded}";

			var response = await _httpClientProviderService.GetAsync(url);

			if (response.StatusCode != HttpStatusCode.OK)
				return null;

			var options = new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			};
			forecastResponse = JsonSerializer.Deserialize<ForecastResponse>(response.Content.ReadAsStream(), options)!;

			return forecastResponse;
		}

		public async Task<ForecastResponse?> GetForecastByCityAsync(string name, string timezone = "Europe/Warsaw")
		{
			ForecastResponse forecastResponse;
			string timezoneEncoded = HttpUtility.UrlEncode(timezone);

			var url = $"{_configurationProviderService.Configuration.ServerAddress}/api/get-forecast-by-city?name={name}&timezone={timezoneEncoded}";

			var response = await _httpClientProviderService.GetAsync(url);

			if (response.StatusCode != HttpStatusCode.OK)
				return null;

			var options = new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			};
			forecastResponse = JsonSerializer.Deserialize<ForecastResponse>(response.Content.ReadAsStream(), options)!;

			return forecastResponse;
		}
	}
}
