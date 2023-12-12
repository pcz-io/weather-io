using MudBlazor;
using WeatherIO.Common.Data.Interfaces;
using WeatherIO.Common.Data.Models;

namespace WeatherIO.Common.Data.Services
{
    /// <summary>
    /// This class is used to provide configuration data to the application
    /// </summary>
    public class ConfigurationProviderService : IConfigurationProviderService
    {
        /// <summary>
        /// This is the data provider service used to store and retrieve data
        /// </summary>
        private readonly IDataProviderService _dataProviderService;
        /// <summary>
        /// This is the configuration object used to store and retrieve configuration data
        /// </summary>
        public Configuration Configuration { get; set; } = new Configuration();
        /// <summary>
        /// This is the theme provider used to store and retrieve theme data
        /// </summary>
        public MudThemeProvider MudThemeProvider { get; set; } = new MudThemeProvider();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dataProviderService">The data provider service used to store and retrieve data</param>
        public ConfigurationProviderService(IDataProviderService dataProviderService)
        {
            _dataProviderService = dataProviderService;
        }

        /// <summary>
        /// This method loads the configuration data from storage
        /// </summary>
        /// <returns>A task that represents the asynchronous operation</returns>
        public async Task LoadConfigurationAsync()
        {
            Configuration.ServerAddress = await _dataProviderService.GetData("serveraddress") ?? "https://localhost:443";
            Configuration.Theme = await _dataProviderService.GetData("theme") ?? string.Empty;
            Configuration.JwtToken = await _dataProviderService.GetData("jwttoken") ?? string.Empty;
            Configuration.ForecastModel = await _dataProviderService.GetData("forecastmodel") ?? "dwdicon";
            await SetTheme(Configuration.Theme);
        }
        
        /// <summary>
        /// This method sets Jwt Bearer token
        /// </summary>
        /// <param name="jwtToken"></param>
        /// <returns></returns>
        public async Task SetJwtToken(string jwtToken)
        {
            await _dataProviderService.SetData("jwttoken", jwtToken);
            Configuration.JwtToken = jwtToken;
        }

        /// <summary>
        /// This method deletes Jwt Bearer token
        /// </summary>
        /// <returns></returns>
        public async Task DeleteJwtToken()
        {
            await _dataProviderService.DeleteData("jwttoken");
            Configuration.JwtToken = null;
        }

        /// <summary>
        /// This method gets Jwt Bearer token
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetJwtToken()
        {
            if(string.IsNullOrEmpty(Configuration.JwtToken))
                Configuration.JwtToken = await _dataProviderService.GetData("jwttoken") ?? string.Empty;
            return Configuration.JwtToken;
        }

        /// <summary>
        /// This method sets the server address
        /// </summary>
        /// <param name="address">The server address</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public async Task SetSerwerAddressAsync(string address)
        {
            await _dataProviderService.SetData("serveraddress", address);
            Configuration.ServerAddress = address;
        }

        /// <summary>
        /// This method gets the server address
        /// </summary>
        /// <returns>The server address</returns>
        public Task<string> GetSerwerAddressAsync()
        {
            return Task.FromResult(Configuration.ServerAddress == string.Empty ? "https://localhost:443" : Configuration.ServerAddress);
        }

        /// <summary>
        /// This method gets the theme
        /// </summary>
        /// <returns>The theme</returns>
        public Task<int> GetThemeAsync()
        {
            return Task.FromResult(Configuration.Theme == string.Empty ? 2 : int.Parse(Configuration.Theme));
        }

        /// <summary>
        /// This method gets the current theme
        /// </summary>
        /// <returns>The boolean value of the current theme</returns>
        public async Task<bool> GetCurrentThemeAsync()
        {
            return Configuration.Theme switch
            {
                "0" => false,
                "1" => true,
                _ => await MudThemeProvider.GetSystemPreference(),
            };
        }

        /// <summary>
        /// This method sets the theme
        /// </summary>
        /// <param name="theme">The theme</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public async Task SetTheme(string theme)
        {
            MudThemeProvider!.IsDarkMode = theme switch
            {
                "0" => false,
                "1" => true,
                _ => await MudThemeProvider.GetSystemPreference(),
            };
            Configuration.Theme = theme;
            MudThemeProvider!.Theme = new();
            await SaveTheme(theme);
        }

        /// <summary>
        /// This method saves the theme
        /// </summary>
        /// <param name="theme">The theme</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        private async Task SaveTheme(string theme)
        {
            await _dataProviderService.SetData("theme", theme);
        }

        /// <summary>
        /// This method sets the forecast model
        /// </summary>
        /// <returns> The forecast model</returns>
        public Task<string> GetForecastModelAsync()
        {
            return Task.FromResult(Configuration.ForecastModel == string.Empty ? "dwdicon" : Configuration.ForecastModel);
        }

        /// <summary>
        /// This method gets the forecast model
        /// </summary>
        /// <param name="forecastModel"> The forecast model</param>
        /// <returns> A task that represents the asynchronous operation</returns>
        public async Task SetForecastModel(string forecastModel)
        {
            await _dataProviderService.SetData("forecastmodel", forecastModel);
            Configuration.ForecastModel = forecastModel;
        }
    }
}
