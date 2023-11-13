using MudBlazor.Services;
using WeatherIO.App.Data.Services;
using WeatherIO.Common.Data.Interfaces;
using WeatherIO.Common.Data.Services;

namespace WeatherIO.App
{
    /// <summary>
    /// Main class for the application
    /// </summary>
    public static class MauiProgram
    {
        /// <summary>
        /// The entry point for the application
        /// </summary>
        /// <returns>The MauiApp</returns>
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();
#if DEBUG
		    builder.Services.AddBlazorWebViewDeveloperTools();
#endif
            builder.Services.AddMudServices();
            builder.Services.AddHttpClient();

            builder.Services.AddSingleton<IDataProviderService, DataProviderServiceMaui>();
            builder.Services.AddSingleton<IHttpClientProviderService, HttpClientProviderServiceMaui>();
            builder.Services.AddSingleton<IConfigurationProviderService, ConfigurationProviderService>();

            return builder.Build();
        }
    }
}