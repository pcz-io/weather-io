using System.Net;
using System.Text.Json;
using System.Web;
using WeatherIO.Common.Data.Interfaces;
using WeatherIO.Common.Data.Models.APIResponses;
using System.Globalization;

namespace WeatherIO.Common.Data.Services
{
	/// <summary>
	/// Weather forecast provider service
	/// </summary>
	public class WeatherForecastProviderService : IWeatherForecastProviderService
	{
		/// <summary>
		/// Configuration provider service
		/// </summary>
		private readonly IConfigurationProviderService _configurationProviderService;
		/// <summary>
		/// Http client provider service
		/// </summary>
		private readonly IHttpClientProviderService _httpClientProviderService;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="configurationProviderService"></param>
		/// <param name="httpClientProviderService"></param>
		public WeatherForecastProviderService(IConfigurationProviderService configurationProviderService, IHttpClientProviderService httpClientProviderService)
		{
			_configurationProviderService = configurationProviderService;
			_httpClientProviderService = httpClientProviderService;
		}

		/// <summary>
		/// Gets forecast response from the server
		/// </summary>
		/// <param name="latitude"> Latitude of the location </param>
		/// <param name="longitude"> Longitude of the location </param>
		/// <param name="timezone"> Timezone of the location </param>
		/// <returns></returns>
		public async Task<ForecastResponse?> GetForecasAsync(double latitude, double longitude, string timezone = "Europe/Warsaw")
		{
			ForecastResponse forecastResponse;
			string timezoneEncoded = HttpUtility.UrlEncode(timezone);
			string latitudeEncoded = latitude.ToString(CultureInfo.InvariantCulture);
			string longitudeEncoded = longitude.ToString(CultureInfo.InvariantCulture);

			var url = $"{_configurationProviderService.Configuration.ServerAddress}/api/get-forecast?latitude={latitudeEncoded}&longitude={longitudeEncoded}&timezone={timezoneEncoded}&model={_configurationProviderService.Configuration.ForecastModel}";

			var response = await _httpClientProviderService.HttpClient.GetAsync(url);

			if (response.StatusCode != HttpStatusCode.OK)
				return null;

			var options = new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			};
			forecastResponse = JsonSerializer.Deserialize<ForecastResponse>(response.Content.ReadAsStream(), options)!;

			return forecastResponse;
		}

		/// <summary>
		/// Gets forecast response from the server
		/// </summary>
		/// <param name="name"> Name of the location </param>
		/// <param name="timezone"> Timezone of the location </param>
		/// <returns> Forecast response </returns>
		public async Task<ForecastResponse?> GetForecastByCityAsync(string name, string timezone = "Europe/Warsaw")
		{
			ForecastResponse forecastResponse;
			string timezoneEncoded = HttpUtility.UrlEncode(timezone);

			var url = $"{_configurationProviderService.Configuration.ServerAddress}/api/get-forecast-by-city?name={name}&timezone={timezoneEncoded}&model={_configurationProviderService.Configuration.ForecastModel}";

			var response = await _httpClientProviderService.HttpClient.GetAsync(url);

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
