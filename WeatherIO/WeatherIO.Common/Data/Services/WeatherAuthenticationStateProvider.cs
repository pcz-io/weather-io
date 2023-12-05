using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using WeatherIO.Common.Data.Interfaces;

namespace WeatherIO.Common.Data.Services
{
    /// <summary>
    /// This class is a extension of AuthenticationStateProvider which is used to
    /// integrate server API authentication method with the client.
    /// </summary>
    public class WeatherAuthenticationStateProvider : AuthenticationStateProvider
    {
        /// <summary>
        /// Dependency injected IConfigurationProviderService service
        /// </summary>
        private readonly IConfigurationProviderService _configurationProvider;

        /// <summary>
        /// Dependency injected IHttpClientProviderService service
        /// </summary>
        private readonly IHttpClientProviderService _httpClientProvider;

        /// <summary>
        /// Constructor with dependency injected services
        /// </summary>
        /// <param name="configurationProvider"></param>
        /// <param name="httpClientProvider"></param>
        public WeatherAuthenticationStateProvider(IConfigurationProviderService configurationProvider, IHttpClientProviderService httpClientProvider)
        {
            _configurationProvider = configurationProvider;
            _httpClientProvider = httpClientProvider;
        }

        /// <summary>
        /// Custom authentication state provider.
        /// Used to create claims from Jwt token
        /// </summary>
        /// <returns></returns>
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _configurationProvider.GetJwtToken();

            var identity = new ClaimsIdentity();
            _httpClientProvider.HttpClient.DefaultRequestHeaders.Authorization = null;

            if (!string.IsNullOrEmpty(token))
            {
                identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
                _httpClientProvider.HttpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token.Replace("\"", ""));
            }

            var user = new ClaimsPrincipal(identity);
            var state = new AuthenticationState(user);

            NotifyAuthenticationStateChanged(Task.FromResult(state));

            return state;
        }

        /// <summary>
        /// Parses Jwt token into claims.
        /// </summary>
        /// <param name="jwt"></param>
        /// <returns></returns>
        private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
        }

        /// <summary>
        /// Helper method for parsing base64 encoding.
        /// </summary>
        /// <param name="base64"></param>
        /// <returns></returns>
        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}
