using WeatherIO.Common.Data.Models;

namespace WeatherIO.Common.Data.Interfaces
{
    public interface IFovouriteProviderService
    {
        Task<bool> DeleteFavouriteAsync(FavouriteLocationModel model);
        Task<List<FavouriteLocationModel>?> GetFavouritesAsync();
        Task<bool> SetOrUpdateFavouriteAsync(FavouriteLocationModel model);
    }
}