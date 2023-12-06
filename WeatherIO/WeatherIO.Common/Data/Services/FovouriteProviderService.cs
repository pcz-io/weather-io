using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WeatherIO.Common.Data.Interfaces;
using WeatherIO.Common.Data.Models;

namespace WeatherIO.Common.Data.Services
{
    /// <summary>
    /// Favourite provider service
    /// </summary>
    public class FovouriteProviderService : IFovouriteProviderService
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
        public FovouriteProviderService(IConfigurationProviderService configurationProviderService, IHttpClientProviderService httpClientProviderService)
        {
            _configurationProviderService = configurationProviderService;
            _httpClientProviderService = httpClientProviderService;
        }

        /// <summary>
        /// Gets favourite locations from the server
        /// </summary>
        /// <returns> List of favourite locations </returns>
        public async Task<List<FavouriteLocationModel>?> GetFavouritesAsync()
        {
            var url = $"{_configurationProviderService.Configuration.ServerAddress}/api/get-favourite-locations";

            var response = await _httpClientProviderService.HttpClient.GetAsync(url);

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var favourites = JsonSerializer.Deserialize<List<FavouriteLocationModel>>(response.Content.ReadAsStream(), options)!;

            return favourites;
        }

        /// <summary>
        /// Sets or updates favourite location
        /// </summary>
        /// <param name="model"> Favourite location model </param>
        /// <returns> True if success </returns>
        public async Task<bool> SetOrUpdateFavouriteAsync(FavouriteLocationModel model)
        {
            var url = $"{_configurationProviderService.Configuration.ServerAddress}/api/add-update-favourite-location";

            var result = await _httpClientProviderService.HttpClient.PostAsJsonAsync(url, model);

            return result.IsSuccessStatusCode;
        }

        /// <summary>
        /// Deletes favourite location
        /// </summary>
        /// <param name="model"> Favourite location model </param>
        /// <returns> True if success </returns>
        public async Task<bool> DeleteFavouriteAsync(FavouriteLocationModel model)
        {
            var url = $"{_configurationProviderService.Configuration.ServerAddress}/api/delete-favourite-location";

            var result = await _httpClientProviderService.HttpClient.PostAsJsonAsync(url, model);

            return result.IsSuccessStatusCode;
        }
    }
}
