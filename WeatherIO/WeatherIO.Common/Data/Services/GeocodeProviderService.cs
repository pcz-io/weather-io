using System.Net;
using System.Text.Json;
using System.Web;
using WeatherIO.Common.Data.Interfaces;
using WeatherIO.Common.Data.Models.APIResponses;

namespace WeatherIO.Common.Data.Services
{
	public class GeocodeProviderService : IGeocodeProviderService
	{
		private readonly IConfigurationProviderService _configurationProviderService;
		private readonly IHttpClientProviderService _httpClientProviderService;

		public GeocodeProviderService(IHttpClientProviderService httpClientProviderService, IConfigurationProviderService configurationProviderService)
		{
			_httpClientProviderService = httpClientProviderService;
			_configurationProviderService = configurationProviderService;
		}

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
