using WeatherIO.Common.Data.Models;

namespace WeatherIO.Common.Data.Interfaces
{
    public interface IWeatherForecastService
    {
        Task<WeatherForecast[]> GetForecastAsync(DateTime startDate);
    }
}