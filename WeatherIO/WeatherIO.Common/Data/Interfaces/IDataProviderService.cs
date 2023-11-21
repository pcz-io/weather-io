namespace WeatherIO.Common.Data.Interfaces
{
    /// <summary>
    /// Interface for data provider service
    /// </summary>
    public interface IDataProviderService
    {
        /// <summary>
        /// Set data in data provider
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        /// <returns>Task</returns>
        public Task SetData(string key, string value);

        /// <summary>
        /// Get data from data provider
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>Value</returns>
        public Task<string> GetData(string key);

        /// <summary>
        /// Remove data from data provider
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task DeleteData(string key);
    }
}
