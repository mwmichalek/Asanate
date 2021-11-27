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
using Mwm.MyQ.Common.Utils;
using Mwm.MyQ.Data.Utils;
using Mwm.MyQ.Persistance.Shared;
using Mwm.Asana.Service.Utils;
using Mwm.MyQ.Application.Utils;
using Fluxor;
using System.Reflection;
using Mwm.MyQ.Client.Service.Facades;
using Mwm.MyQ.Client.Service.Storage;
using Mwm.MyQ.Client.Service.Utils;

namespace Mwm.MyQ.Client.Blazor {
    public class Program {
        public static async Task Main(string[] args) {

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTA5OTE4QDMxMzkyZTMzMmUzMGZ4QnFHbGNEVFI4ZUcwYzZnVUJnQkFEcmxKUlFmQWl4RDc4VDdKcFRmcE09");
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
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


