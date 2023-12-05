using System.Text.Json.Serialization;

namespace WeatherIO.Server.Data.Models.OpenMeteo
{
    /// <summary>
    /// This class represents OpenMeteo Geocoding API response
    /// </summary>
    public class GeocodingModel
    {
        /// <summary>
        /// Represents JSON object that stores one city data
        /// </summary>
        public class GeocodingResult
        {
            /// <summary>
            /// Id of the city
            /// </summary>
            [JsonPropertyName("id")]
            public int Id { get; set; }

            /// <summary>
            /// Name of the city
            /// </summary>
            [JsonPropertyName("name")]
            public string Name { get; set; }

            /// <summary>
            /// Latitude of the city
            /// </summary>
            [JsonPropertyName("latitude")]
            public double Latitude { get; set; }

            /// <summary>
            /// Longitude of the city
            /// </summary>
            [JsonPropertyName("longitude")]
            public double Longitude { get; set; }

            /// <summary>
            /// Elevation of the city
            /// </summary>
            [JsonPropertyName("elevation")]
            public double Elevation { get; set; }

            /// <summary>
            /// Time zone of the city
            /// </summary>
            [JsonPropertyName("timezone")]
            public string Timezone { get; set; }

            /// <summary>
            /// Population of the city
            /// </summary>
            [JsonPropertyName("population")]
            public int Population { get; set; }
        }

        /// <summary>
        /// List of all query matching cities
        /// </summary>
        [JsonPropertyName("results")]
        public IList<GeocodingResult> Results { get; set; }

        /// <summary>
        /// OpenMeteo response generation time
        /// </summary>
        [JsonPropertyName("generationtime_ms")]
        public double GenerationTime { get; set; }
    }
}
