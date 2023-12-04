using WeatherIO.Common.Data.Models.APIResponses;

namespace WeatherIO.Common.Data.Interfaces
{
    public interface IAirQualityProviderService
    {
        Task<AirQualityResponse?> GetAirQualityAsync(double latitude, double longitude, string timezone = "Europe/Warsaw");
    }
}
