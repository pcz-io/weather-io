using WeatherIO.Common.Data.Interfaces;

namespace WeatherIO.App.Data.Services
{
    internal class DataProviderServiceMaui : IDataProviderService
    {
        public Task<string> GetData(string key)
        {
            string value = Preferences.Default.Get(key, string.Empty);
            return Task.FromResult(value);
        }

        public Task SetData(string key, string value)
        {
            Preferences.Default.Set(key, value);
            return Task.CompletedTask;
        }
    }
}
