namespace WeatherIO.Common.Data.Models.APIResponses
{
    /// <summary>
    /// Represents JSON data returned from air-quality endpoint
    /// </summary>
    public class AirQualityResponse
    {
        /// <summary>
        /// Air quality index
        /// </summary>
        public int AirQualityIndex { get; set; }

        /// <summary>
        /// Air quality index string
        /// </summary>
        public string AirQualityString { get; set; }
    }
}
