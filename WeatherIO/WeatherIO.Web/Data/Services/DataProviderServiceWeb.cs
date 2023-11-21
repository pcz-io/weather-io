using Blazored.LocalStorage;
using WeatherIO.Common.Data.Interfaces;

namespace WeatherIO.Web.Data.Services
{
    /// <summary>
    /// This class is used to provide data from local storage
    /// </summary>
    public class DataProviderServiceWeb : IDataProviderService
    {
        /// <summary>
        /// Local storage service
        /// </summary>
        private readonly ILocalStorageService _localStorageService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="localStorageService">Local storage service</param>
        public DataProviderServiceWeb(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        /// <summary>
        /// Remove data from data provider
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task DeleteData(string key)
        {
            await _localStorageService.RemoveItemAsync(key);
        }

        /// <summary>
        /// Get data from local storage
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>Value</returns>
        public async Task<string> GetData(string key)
        {
            return await _localStorageService.GetItemAsync<string>(key);
        }
        
        /// <summary>
        /// Set data in local storage
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        /// <returns>Task</returns>
        public async Task SetData(string key, string value)
        {
            await _localStorageService.SetItemAsync(key, value);
        }
    }
}
