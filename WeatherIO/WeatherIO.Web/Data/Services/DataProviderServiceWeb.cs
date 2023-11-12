using Blazored.LocalStorage;
using WeatherIO.Common.Data.Interfaces;

namespace WeatherIO.Web.Data.Services
{
    public class DataProviderServiceWeb : IDataProviderService
    {
        private readonly ILocalStorageService _localStorageService;

        public DataProviderServiceWeb(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        public async Task<string> GetData(string key)
        {
            return await _localStorageService.GetItemAsync<string>(key);
        }

        public async Task SetData(string key, string value)
        {
            await _localStorageService.SetItemAsync(key, value);
        }
    }
}
