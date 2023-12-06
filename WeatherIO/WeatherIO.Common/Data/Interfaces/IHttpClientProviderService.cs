namespace WeatherIO.Common.Data.Interfaces
{
    /// <summary>
    /// Interface for a service that provides an HttpClient
    /// </summary>
    public interface IHttpClientProviderService
    {
        /// <summary>
        /// Instance of HttpClient.
        /// Necessary to use when sending data to authorized endpoints.
        /// </summary>
        public HttpClient HttpClient { get; }
    }
}
