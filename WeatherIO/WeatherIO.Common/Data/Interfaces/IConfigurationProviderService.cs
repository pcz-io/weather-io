﻿using MudBlazor;
using WeatherIO.Common.Data.Models;

namespace WeatherIO.Common.Data.Interfaces
{
    /// <summary>
    /// Interface for configuration provider service
    /// </summary>
    public interface IConfigurationProviderService
    {
        /// <summary>
        /// Configuration object
        /// </summary>
        public Configuration Configuration { get; set; }
        /// <summary>
        /// MudThemeProvider object
        /// </summary>
        public MudThemeProvider MudThemeProvider { get; set; }

        /// <summary>
        /// This method loads the configuration data from storage
        /// </summary>
        /// <returns>Task</returns>
        public Task LoadConfigurationAsync();

        /// <summary>
        /// This method sets the server address
        /// </summary>
        /// <param name="address">The server address</param>
        /// <returns>Task</returns>
        public Task SetSerwerAddressAsync(string address);

        /// <summary>
        /// This method gets the server address
        /// </summary>
        /// <returns>The server address</returns>
        public Task<string> GetSerwerAddressAsync();

        /// <summary>
        /// This method gets the theme
        /// </summary>
        /// <returns>The theme</returns>
        public Task<int> GetThemeAsync();

        /// <summary>
        /// This method gets the current theme
        /// </summary>
        /// <returns>The boolean value of the current theme</returns>
        public Task<bool> GetCurrentThemeAsync();

        /// <summary>
        /// This method sets the theme
        /// </summary>
        /// <param name="theme">The theme</param>
        /// <returns>Task</returns>
        public Task SetTheme(string theme);

        /// <summary>
        /// This method sets Jwt Bearer token
        /// </summary>
        /// <param name="jwtToken"></param>
        /// <returns></returns>
        Task SetJwtToken(string jwtToken);

		/// <summary>
		/// This method gets Jwt Bearer token
		/// </summary>
		/// <returns></returns>
		Task<string> GetJwtToken();

		/// <summary>
		/// This method deletes Jwt Bearer token
		/// </summary>
		/// <returns></returns>
		Task DeleteJwtToken();

        /// <summary>
        /// This method sets the forecast model
        /// </summary>
        /// <returns> The forecast model</returns>
        public Task<string> GetForecastModelAsync();

        /// <summary>
        /// This method gets the forecast model
        /// </summary>
        /// <param name="forecastModel"> The forecast model</param>
        /// <returns> A task that represents the asynchronous operation</returns>
        public Task SetForecastModel(string forecastModel);
    }
}
