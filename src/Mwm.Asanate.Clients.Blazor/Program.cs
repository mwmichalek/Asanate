using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Mwm.Asana.Model;
using Mwm.Asana.Service;
using Mwm.Asanate.Service;
using Syncfusion.Blazor;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.Asanate.Clients.Blazor {
    public class Program {
        public static async Task Main(string[] args) {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NDk3NTY2QDMxMzkyZTMyMmUzMEtidFArTFZsYWhvNGlHcXpaRHkwc0xqZGV3T3EyK0hFWVhHamtzOUVKRVU9");
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            var myCloneApiUrl = ConfigurationManager.AppSettings["MyClone.Api.Url"];
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(myCloneApiUrl) });

            builder.Services.AddSyncfusionBlazor();
            builder.Services.AddMsalAuthentication(options => {
                builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
            });

            var host = builder.Build();


            await host.RunAsync();
        }
    }
}
