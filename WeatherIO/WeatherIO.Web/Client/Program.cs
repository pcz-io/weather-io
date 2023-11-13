using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using WeatherIO.Common;
using WeatherIO.Common.Data.Interfaces;
using WeatherIO.Common.Data.Services;
using WeatherIO.Web.Data.Services;

namespace WeatherIO.Web
{
    /// <summary>
    /// Main class for the application
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The entry point for the application
        /// </summary>
        /// <param name="args">The arguments</param>
        /// <returns>The task</returns>
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddMudServices();
            builder.Services.AddBlazoredLocalStorageAsSingleton();
            builder.Services.AddHttpClient();

            builder.Services.AddSingleton<IDataProviderService, DataProviderServiceWeb>();
            builder.Services.AddSingleton<IHttpClientProviderService, HttpClientProviderServiceWeb>();
            builder.Services.AddSingleton<IConfigurationProviderService, ConfigurationProviderService>();

            await builder.Build().RunAsync();
        }
    }
}