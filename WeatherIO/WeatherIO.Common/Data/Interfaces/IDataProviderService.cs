namespace WeatherIO.Common.Data.Interfaces
{
    public interface IDataProviderService
    {
        public Task SetData(string key, string value);

        public Task<string> GetData(string key);
    }
}
