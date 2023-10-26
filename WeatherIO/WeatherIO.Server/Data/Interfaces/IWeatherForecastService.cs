using WeatherIO.Common.Data.Models;

namespace WeatherIO.Server.Data.Interfaces
{
    public interface IWeatherForecastService
    {
        IEnumerable<WeatherForecast> Get();
    }
}
