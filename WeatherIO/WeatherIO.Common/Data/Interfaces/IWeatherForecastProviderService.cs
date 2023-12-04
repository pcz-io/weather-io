using WeatherIO.Common.Data.Models.APIResponses;

namespace WeatherIO.Common.Data.Interfaces
{
    public interface IWeatherForecastProviderService
    {
        Task<ForecastResponse?> GetForecastByCityAsync(string name, string timezone = "Europe/Warsaw");
        Task<ForecastResponse?> GetForecasAsync(double latitude, double longitude, string timezone = "Europe/Warsaw");
    }
}
