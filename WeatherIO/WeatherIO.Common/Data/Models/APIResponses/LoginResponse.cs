namespace WeatherIO.Common.Data.Models.APIResponses
{
    /// <summary>
    /// Represents response data from `login` endpoint
    /// </summary>
    public class LoginResponse
    {
        /// <summary>
        /// Jwt Bearer token
        /// </summary>
        public string Token { get; set; }
    }
}
