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

        /// <summary>
        /// This method is used to get an HttpResponseMessage from a given url
        /// </summary>
        /// <param name="url">The url to get the HttpResponseMessage from</param>
        /// <returns>An HttpResponseMessage from the given url</returns>
        public Task<HttpResponseMessage> GetAsync(string url);

        /// <summary>
        /// This method is used to post data to a given url
        /// </summary>
        /// <param name="url"> The url to post data to</param>
        /// <param name="content"> The data to post to the url</param>
        /// <returns> An HttpResponseMessage from the given url</returns>
        public Task<HttpResponseMessage> PostAsync(string url, HttpContent content);
    }
}
