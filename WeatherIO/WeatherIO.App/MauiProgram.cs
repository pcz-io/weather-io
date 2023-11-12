using Microsoft.AspNetCore.Components.WebView.Maui;
using MudBlazor.Services;
using WeatherIO.App.Data.Services;
using WeatherIO.Common.Data.Interfaces;

namespace WeatherIO.App
{
    public static class MauiProgram
    {
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

            return builder.Build();
        }
    }
}