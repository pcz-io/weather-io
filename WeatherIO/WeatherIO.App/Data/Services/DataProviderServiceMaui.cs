using WeatherIO.Common.Data.Interfaces;

namespace WeatherIO.App.Data.Services
{
    /// <summary>
    /// This class is used to provide data from local storage
    /// </summary>
    internal class DataProviderServiceMaui : IDataProviderService
    {
        /// <summary>
        /// Remove data from local storage
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task DeleteData(string key)
        {
            Preferences.Default.Remove(key);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Get data from local storage
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>Value</returns>
        public Task<string> GetData(string key)
        {
            string value = Preferences.Default.Get(key, string.Empty);
            return Task.FromResult(value);
        }

        /// <summary>
        /// Set data in local storage
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        /// <returns>Task</returns>
        public Task SetData(string key, string value)
        {
            Preferences.Default.Set(key, value);
            return Task.CompletedTask;
        }
    }
}
