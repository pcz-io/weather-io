using WeatherIO.Common.Data.Interfaces;

namespace WeatherIO.App.Data.Services
{
    /// <summary>
    /// This class is used to provide an HttpClient for the Maui project
    /// </summary>
    internal class HttpClientProviderServiceMaui : IHttpClientProviderService
    {
        /// <summary>
        /// This method is used to get an HttpResponseMessage from a given url
        /// </summary>
        /// <param name="url">The url to get the HttpResponseMessage from</param>
        /// <returns>An HttpResponseMessage from the given url</returns>
        public Task<HttpResponseMessage> GetAsync(string url)
        {
            var handler = new HttpClientHandler
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    return true;
                }
            };

            var httpClinet = new HttpClient(handler);
            return httpClinet.GetAsync(url);
        }
    }
}
