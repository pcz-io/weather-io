using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using WeatherIO.Common.Data.Interfaces;
using WeatherIO.Common.Data.Models;
using WeatherIO.Common.Data.Models.APIResponses;

namespace WeatherIO.Common.Data.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IConfigurationProviderService _configurationProvider;
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly IHttpClientProviderService _clientProviderService;

        private string? username;

        public AuthenticationService(IConfigurationProviderService configurationProvider, AuthenticationStateProvider authStateProvider, IHttpClientProviderService clientProviderService)
        {
            _configurationProvider = configurationProvider;
            _authStateProvider = authStateProvider;
            _clientProviderService = clientProviderService;
        }

        /// <summary>
        /// Registers user
        /// </summary>
        /// <param name="registerModel"></param>
        /// <returns></returns>
        public async Task<bool> Register(RegisterModel registerModel)
        {
            var url = $"{_configurationProvider.Configuration.ServerAddress}/api/register";
            var result = await _clientProviderService.HttpClient.PostAsJsonAsync(url, registerModel);
            return result.IsSuccessStatusCode;
        }

        /// <summary>
        /// Logins to the server.
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns>True if succeeded otherwise false</returns>
        public async Task<bool> Login(LoginModel loginModel)
        {
            var url = $"{_configurationProvider.Configuration.ServerAddress}/api/login";
            var result = await _clientProviderService.HttpClient.PostAsJsonAsync(url, loginModel);
            if (result.IsSuccessStatusCode)
            {
                var loginResponse = await result.Content.ReadFromJsonAsync<LoginResponse>();
                if (loginResponse == null)
                    return false;
                await _configurationProvider.SetJwtToken(loginResponse.Token);
                await _authStateProvider.GetAuthenticationStateAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Logs out user, i.e. deletes Jwt token from storage.
        /// </summary>
        /// <returns></returns>
        public async Task Logout()
        {
            username = null;
            await _configurationProvider.DeleteJwtToken();
            await _authStateProvider.GetAuthenticationStateAsync();
        }

        /// <summary>
        /// Gets logged user username.
        /// </summary>
        /// <returns></returns>
        public async Task<string?> GetUsername()
        {
            if (!string.IsNullOrEmpty(username))
                return username;

            // Without it does not work
            // Do not remove or question
            await _configurationProvider.GetJwtToken();

            var url = $"{await _configurationProvider.GetSerwerAddressAsync()}/api/get-user-info";
            var result = await _clientProviderService.HttpClient.GetAsync(url);
            if (result.IsSuccessStatusCode)
            {
                var userInfo = await result.Content.ReadFromJsonAsync<UserInfoResponse>();
                if (userInfo == null)
                    return null;
                username = userInfo.UserName;
                return userInfo.UserName;
            }
            else
            {
                return null;
            }
        }
    }
}
