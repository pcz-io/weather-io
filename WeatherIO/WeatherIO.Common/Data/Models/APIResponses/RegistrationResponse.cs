namespace WeatherIO.Common.Data.Models.APIResponses
{
    /// <summary>
    /// Represents response data from `register` endpoint
    /// </summary>
    public class RegistrationResponse
    {
        /// <summary>
        /// Status of the registration
        /// Could be: "Error" or "Success"
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Registration message
        /// </summary>
        public string Message { get; set; }
    }
}
