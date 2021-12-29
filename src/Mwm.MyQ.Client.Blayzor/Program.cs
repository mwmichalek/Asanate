using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Syncfusion.Blazor;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Mwm.MyQ.Common.Utils;
using Mwm.MyQ.Client.Service.Utils;

namespace Mwm.MyQ.Client.Blayzor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            //Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTA5OTE4QDMxMzkyZTMzMmUzMGZ4QnFHbGNEVFI4ZUcwYzZnVUJnQkFEcmxKUlFmQWl4RDc4VDdKcFRmcE09");
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTU1NTM3QDMxMzkyZTM0MmUzMG9iRVhabWpuRHVCTyt6WGwvOWlqczdQMUJwa0pEV0tvcVp3MG1YdnB1ZHM9");
            // Add your Syncfusion license key for Blazor platform with corresponding Syncfusion NuGet version referred in project. For more information about license key see https://help.syncfusion.com/common/essential-studio/licensing/license-key.
            // Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Add your license key here"); 
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            //builder.Services.AddSyncfusionBlazor();
            //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            //await builder.Build().RunAsync();

            var services = builder.Services;
            var myCloneApiUrl = builder.Configuration["MyClone:Api:Url"];
            //var connectionString = builder.Configuration["ConnectionStrings:DatabaseContext"];
            Console.WriteLine($"Middle Tier: [{myCloneApiUrl}]");
            //Console.WriteLine($"Database: [{connectionString}]");

            services.AddTransient(sp => new HttpClient { BaseAddress = new Uri("https://localhost:5001") });

            var configuration = services.AddConfigurationWithUserSecrets();
            services.AddLogging();
            services.AddClientServices();

            builder.Services.AddSyncfusionBlazor();

            await builder.Build().RunAsync();
        }
    }
}
