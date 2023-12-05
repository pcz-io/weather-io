namespace WeatherIO.Common.Data.Models.APIResponses
{
    /// <summary>
    /// Represents response data from `geocode` and `geocode-by-id` endpoints
    /// </summary>
    public class GeocodeResponse
    {
        /// <summary>
        /// Id of the city
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the city
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// Longitude of the city
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// Latitude of the city
        /// </summary>
        public double Latitude { get; set; }
    }
}
