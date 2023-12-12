using WeatherIO.Common.Data.Interfaces;

namespace WeatherIO.Web.Data.Services
{
    /// <summary>
    /// This class is used to provide an HttpClient for the Web project
    /// </summary>
    internal class HttpClientProviderServiceWeb : IHttpClientProviderService
    {
        private HttpClient _httpClient;

        /// <summary>
        /// Instance of HttpClient.
        /// Necessary to use when sending data to authorized endpoints.
        /// </summary>
        public HttpClient HttpClient => _httpClient;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="httpClientFactory">The HttpClientFactory is used to create an HttpClient</param>
        public HttpClientProviderServiceWeb(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }
    }
}
