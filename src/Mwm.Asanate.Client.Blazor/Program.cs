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
using Mwm.Asanate.Common.Utils;
using Mwm.Asanate.Data.Utils;
using Mwm.Asanate.Persistance.Shared;
using Mwm.Asana.Service.Utils;
using Mwm.Asanate.Application.Utils;

namespace Mwm.Asanate.Client.Blazor {
    public class Program {
        public static async Task Main(string[] args) {

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NDk3NTY2QDMxMzkyZTMyMmUzMEtidFArTFZsYWhvNGlHcXpaRHkwc0xqZGV3T3EyK0hFWVhHamtzOUVKRVU9");
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.RootComponents.Add<App>("#app");

            var myCloneApiUrl = builder.Configuration["MyClone:Api:Url"];
            Console.WriteLine($"Middle Tier: [{myCloneApiUrl}]");

            var services = builder.Services;

            services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(myCloneApiUrl) });

            var configuration = services.AddConfigurationWithUserSecrets();
            services.AddLogging();
            services.AddDatabaseContext(configuration);
            services.AddRepositories();
            services.AddAsanaServices();
            services.AddMediatR();

            builder.Services.AddSyncfusionBlazor();

            await builder.Build().RunAsync();
        }
    }
}


