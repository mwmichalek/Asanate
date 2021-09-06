using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Mwm.Asanate.Model;
using Mwm.Asanate.Service.AsanaApi;
using Mwm.Asanate.Service.TaskMaster;
using Syncfusion.Blazor;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.Asanate.Clients.Blazor {
    public class Program {
        public static async Task Main(string[] args) {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NDk3NTY2QDMxMzkyZTMyMmUzMEtidFArTFZsYWhvNGlHcXpaRHkwc0xqZGV3T3EyK0hFWVhHamtzOUVKRVU9");
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddSingleton<IAsanaHttpClientFactory, AsanaHttpClientFactory>();
            builder.Services.AddSingleton<IAsanaService<Tsk>, MemoryCacheAsanaService<Tsk>>();
            builder.Services.AddSingleton<IAsanaService<Project>, MemoryCacheAsanaService<Project>>();
            builder.Services.AddSingleton<IAsanaService<Section>, SectionMemoryCacheAsanaService>();
            builder.Services.AddSingleton<ITaskMasterService, TaskMasterService>();
            builder.Services.AddSyncfusionBlazor();
            builder.Services.AddMsalAuthentication(options => {
                builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
            });

            var host = builder.Build();
            await host.Services.GetService<IAsanaService<Tsk>>().Initialize();
            await host.Services.GetService<IAsanaService<Project>>().Initialize();
            await host.Services.GetService<IAsanaService<Section>>().Initialize();

            await host.RunAsync();
        }
    }
}
