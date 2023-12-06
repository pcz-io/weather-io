using WeatherIO.Common.Data.Models;

namespace WeatherIO.Common.Data.Interfaces
{
    /// <summary>
    /// Favourite provider service
    /// </summary>
    public interface IFovouriteProviderService
    {
        /// <summary>
        /// Deletes favourite location
        /// </summary>
        /// <param name="model"> Favourite location model </param>
        /// <returns> True if success </returns>
        Task<bool> DeleteFavouriteAsync(FavouriteLocationModel model);
        /// <summary>
        /// Gets favourite locations from the server
        /// </summary>
        /// <returns> List of favourite locations </returns>
        Task<List<FavouriteLocationModel>?> GetFavouritesAsync();
        /// <summary>
        /// Sets or updates favourite location
        /// </summary>
        /// <param name="model"> Favourite location model </param>
        /// <returns> True if success </returns>
        Task<bool> SetOrUpdateFavouriteAsync(FavouriteLocationModel model);
    }
}