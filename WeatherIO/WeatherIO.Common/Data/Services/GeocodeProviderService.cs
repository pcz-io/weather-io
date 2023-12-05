using System.Net;
using System.Text.Json;
using System.Web;
using WeatherIO.Common.Data.Interfaces;
using WeatherIO.Common.Data.Models.APIResponses;

namespace WeatherIO.Common.Data.Services
{
	/// <summary>
	/// Geocode provider service
	/// </summary>
	public class GeocodeProviderService : IGeocodeProviderService
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
		/// <param name="httpClientProviderService"></param>
		/// <param name="configurationProviderService"></param>
		public GeocodeProviderService(IHttpClientProviderService httpClientProviderService, IConfigurationProviderService configurationProviderService)
		{
			_httpClientProviderService = httpClientProviderService;
			_configurationProviderService = configurationProviderService;
		}

		/// <summary>
		/// Gets geocode response from the server
		/// </summary>
		/// <param name="name"> Name of the location </param>
		/// <returns> Geocode response </returns>
		public async Task<GeocodeResponse?> GetGeocodeAsync(string name)
		{
			GeocodeResponse[] geocodeResponse;

			var url = $"{_configurationProviderService.Configuration.ServerAddress}/api/geocode?name={name}";

			var response = await _httpClientProviderService.GetAsync(url);

			if (response.StatusCode != HttpStatusCode.OK)
				return null;

			var options = new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			};

			geocodeResponse = JsonSerializer.Deserialize<GeocodeResponse[]>(response.Content.ReadAsStream(), options)!;

			return geocodeResponse[0];
		}
	}
}
