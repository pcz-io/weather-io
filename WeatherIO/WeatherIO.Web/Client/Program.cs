using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using WeatherIO.Common;
using WeatherIO.Common.Data.Interfaces;

using WeatherIO.Web.Data.Services;
using Blazored.LocalStorage;

namespace WeatherIO.Web
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddMudServices();

            builder.Services.AddBlazoredLocalStorageAsSingleton();

            builder.Services.AddSingleton<IDataProviderService, DataProviderServiceWeb>();


            await builder.Build().RunAsync();
        }
    }
}