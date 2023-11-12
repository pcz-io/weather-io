using WeatherIO.Common.Data.Interfaces;

namespace WeatherIO.Web.Data.Services
{
    internal class HttpClientProviderServiceWeb : IHttpClientProviderService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpClientProviderServiceWeb(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            var httpClient = _httpClientFactory.CreateClient();
            return await httpClient.GetAsync(url);
        }
    }
}
