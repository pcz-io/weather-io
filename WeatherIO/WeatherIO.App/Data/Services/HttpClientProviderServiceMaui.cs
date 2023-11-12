using WeatherIO.Common.Data.Interfaces;

namespace WeatherIO.App.Data.Services
{
    internal class HttpClientProviderServiceMaui : IHttpClientProviderService
    {
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
