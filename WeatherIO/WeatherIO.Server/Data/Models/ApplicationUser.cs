using Microsoft.AspNetCore.Identity;

namespace WeatherIO.Server.Data.Models
{
    /// <summary>
    /// This class represents user record in database
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// This field represents one to many relation between ApplicationUser and FavouriteLocationModel
        /// </summary>
        public virtual IEnumerable<FavouriteLocation> FavouriteLocations { get; set; } = null!;
    }
}
