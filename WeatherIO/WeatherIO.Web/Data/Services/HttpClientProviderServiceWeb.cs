using WeatherIO.Common.Data.Interfaces;

namespace WeatherIO.Web.Data.Services
{
    /// <summary>
    /// This class is used to provide an HttpClient for the Web project
    /// </summary>
    internal class HttpClientProviderServiceWeb : IHttpClientProviderService
    {
        /// <summary>
        /// The HttpClientFactory is used to create an HttpClient
        /// </summary>
        private readonly IHttpClientFactory _httpClientFactory;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="httpClientFactory">The HttpClientFactory is used to create an HttpClient</param>
        public HttpClientProviderServiceWeb(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// This method is used to get an HttpResponseMessage from a given url
        /// </summary>
        /// <param name="url">The url to get the HttpResponseMessage from</param>
        /// <returns>An HttpResponseMessage from the given url</returns>
        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            var httpClient = _httpClientFactory.CreateClient();
            return await httpClient.GetAsync(url);
        }
    }
}
