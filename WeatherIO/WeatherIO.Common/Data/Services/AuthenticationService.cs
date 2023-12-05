using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using WeatherIO.Common.Data.Interfaces;
using WeatherIO.Common.Data.Models;
using WeatherIO.Common.Data.Models.APIResponses;

namespace WeatherIO.Common.Data.Services
{
    /// <summary>
    /// This service handles authentication with the server
    /// </summary>
    public class AuthenticationService : IAuthenticationService
    {
        /// <summary>
        /// Dependency injected IConfigurationProviderService
        /// </summary>
        private readonly IConfigurationProviderService _configurationProvider;

        /// <summary>
        /// Dependency injected AuthenticationStateProvider
        /// </summary>
        private readonly AuthenticationStateProvider _authStateProvider;

        /// <summary>
        /// Dependency injected IHttpClientProviderService
        /// </summary>
        private readonly IHttpClientProviderService _clientProviderService;

        /// <summary>
        /// Cached username
        /// </summary>
        private string? username;

        /// <summary>
        /// Constructor with dependency injected services
        /// </summary>
        /// <param name="configurationProvider"></param>
        /// <param name="authStateProvider"></param>
        /// <param name="clientProviderService"></param>
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

        /// <summary>
        /// Deletes user account.
        /// </summary>
        /// <returns> True if succeeded otherwise false</returns>
        public async Task<bool> DeleteAccount()
        {
            var url = $"{_configurationProvider.Configuration.ServerAddress}/api/delete-account";
            var result = await _clientProviderService.HttpClient.PostAsync(url, null);
            if (result.IsSuccessStatusCode)
            {
                await Logout();
            }
            return result.IsSuccessStatusCode;
        }

        /// <summary>
        /// Change user password.
        /// </summary>
        /// <param name="model"> Model containing old and new password.</param>
        /// <returns> True if succeeded otherwise false</returns>
        public async Task<bool> ChangePassword(ChangePasswordModel model)
        {
            var url = $"{_configurationProvider.Configuration.ServerAddress}/api/change-password";
            var result = await _clientProviderService.HttpClient.PostAsJsonAsync(url, model);
            return result.IsSuccessStatusCode;
        }
    }
}
