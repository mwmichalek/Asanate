using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.Blazor;

namespace Mwm.Asanate.Client.Blazor {
    public class Program {
        public static async Task Main(string[] args) {

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NDk3NTY2QDMxMzkyZTMyMmUzMEtidFArTFZsYWhvNGlHcXpaRHkwc0xqZGV3T3EyK0hFWVhHamtzOUVKRVU9");
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.RootComponents.Add<App>("#app");

            var myCloneApiUrl = builder.Configuration["MyClone:Api:Url"];
            Console.WriteLine($"Middle Tier: [{myCloneApiUrl}]");
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(myCloneApiUrl) });

            builder.Services.AddSyncfusionBlazor();

            await builder.Build().RunAsync();
        }
    }
}


