namespace WeatherIO.Common.Data.Interfaces
{
    /// <summary>
    /// Interface for a service that provides an HttpClient
    /// </summary>
    public interface IHttpClientProviderService
    {
        /// <summary>
        /// This method is used to get an HttpResponseMessage from a given url
        /// </summary>
        /// <param name="url">The url to get the HttpResponseMessage from</param>
        /// <returns>An HttpResponseMessage from the given url</returns>
        public Task<HttpResponseMessage> GetAsync(string url);
    }
}
