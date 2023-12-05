using WeatherIO.Common.Data.Models.APIResponses;

namespace WeatherIO.Common.Data.Interfaces
{
    /// <summary>
	/// Geocode provider service
	/// </summary>
    public interface IGeocodeProviderService
    {
        /// <summary>
		/// Gets geocode response from the server
		/// </summary>
		/// <param name="name"> Name of the location </param>
		/// <returns> Geocode response </returns>
        Task<GeocodeResponse?> GetGeocodeAsync(string name); 
    }
}
