namespace WeatherIO.Server.Data.Models
{
    /// <summary>
    /// This class is a representation of table in database
    /// </summary>
    public class FavouriteLocation
    {
        /// <summary>
        /// Id of the location
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the location
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Latitude of the location
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Longitude of the location
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// This field represents one to many relation between FavouriteLocation and ApplicationUser
        /// </summary>
        public ApplicationUser User { get; set; }
    }
}
