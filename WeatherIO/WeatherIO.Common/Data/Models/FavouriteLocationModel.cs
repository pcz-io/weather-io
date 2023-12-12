using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherIO.Common.Data.Models
{
    /// <summary>
    /// Stores user favourite location data
    /// </summary>
    public class FavouriteLocationModel
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
    }
}
