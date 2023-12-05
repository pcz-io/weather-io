using WeatherIO.Common.Data.Interfaces;

namespace WeatherIO.App.Data.Services
{
    /// <summary>
    /// This class is used to provide an HttpClient for the Maui project
    /// </summary>
    internal class HttpClientProviderServiceMaui : IHttpClientProviderService
    {
        private HttpClient _httpClient;


		/// <summary>
		/// Instance of HttpClient.
		/// Necessary to use when sending data to authorized endpoints.
		/// </summary>
		public HttpClient HttpClient => _httpClient;

        public HttpClientProviderServiceMaui()
        {
            var handler = new HttpClientHandler
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    return true;
                }
            };

            _httpClient = new HttpClient(handler);
        }

        /// <summary>
        /// This method is used to get an HttpResponseMessage from a given url
        /// </summary>
        /// <param name="url">The url to get the HttpResponseMessage from</param>
        /// <returns>An HttpResponseMessage from the given url</returns>
        public Task<HttpResponseMessage> GetAsync(string url)
        {
            return HttpClient.GetAsync(url);
        }
    }
}
