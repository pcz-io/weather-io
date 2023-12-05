namespace WeatherIO.Common.Data.Models
{
    /// <summary>
    /// This class is used to store the configuration of the application.
    /// </summary>
    public class Configuration
    {
        /// <summary>
        /// The address of the server to connect to.
        /// </summary>
        public string ServerAddress { get; set; } = string.Empty;
        /// <summary>
        /// The theme to use.
        /// </summary>
        public string Theme { get; set; } = string.Empty;
        /// <summary>
        /// The session token for authentication.
        /// </summary>
        public string? JwtToken { get; set; } = null;
        /// <summary>
        /// The forecast model to use.
        /// </summary>
        public string ForecastModel { get; set; } = string.Empty;
    }
}
