using WeatherIO.Common.Data.Models.APIResponses;

namespace WeatherIO.Common.Data.Interfaces
{
    public interface IGeocodeProviderService
    {
        Task<GeocodeResponse?> GetGeocodeAsync(string name); 
    }
}
