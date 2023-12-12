using WeatherIO.Common.Data.Models.APIResponses;

namespace WeatherIO.Common.Data.Interfaces
{
    /// <summary>
    /// This is a service that provides air quality data from external API.
    /// </summary>
    public interface IAirQualityProviderService
    {
        /// <summary>
        /// This method gets air quality data from external API.
        /// </summary>
        /// <param name="latitude"> Latitude of the location. </param>
        /// <param name="longitude"> Longitude of the location. </param>
        /// <param name="timezone"> Timezone of the location. </param>
        /// <returns></returns>
        Task<AirQualityResponse?> GetAirQualityAsync(double latitude, double longitude, string timezone = "Europe/Warsaw");
    }
}
