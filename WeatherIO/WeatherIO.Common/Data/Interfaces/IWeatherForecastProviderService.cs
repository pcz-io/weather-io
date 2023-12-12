using WeatherIO.Common.Data.Models.APIResponses;

namespace WeatherIO.Common.Data.Interfaces
{
    /// <summary>
    /// Weather forecast provider service
    /// </summary>
    public interface IWeatherForecastProviderService
    {
        /// <summary>
		/// Gets forecast response from the server
		/// </summary>
		/// <param name="name"> Name of the location </param>
		/// <param name="timezone"> Timezone of the location </param>
		/// <returns> Forecast response </returns>
        Task<ForecastResponse?> GetForecastByCityAsync(string name, string timezone = "Europe/Warsaw");

        /// <summary>
		/// Gets forecast response from the server
		/// </summary>
		/// <param name="latitude"> Latitude of the location </param>
		/// <param name="longitude"> Longitude of the location </param>
		/// <param name="timezone"> Timezone of the location </param>
		/// <returns></returns>
        Task<ForecastResponse?> GetForecasAsync(double latitude, double longitude, string timezone = "Europe/Warsaw");
    }
}
