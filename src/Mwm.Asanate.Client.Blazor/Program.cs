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
using Fluxor;
using System.Reflection;
using Mwm.Asanate.Client.Blazor.Services;
using Mwm.Asanate.Client.Blazor.Services.State;
using Mwm.Asanate.Client.Blazor.Services.Storage;

namespace Mwm.Asanate.Client.Blazor {
    public class Program {
        public static async Task Main(string[] args) {

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTA5OTE4QDMxMzkyZTMzMmUzMGZ4QnFHbGNEVFI4ZUcwYzZnVUJnQkFEcmxKUlFmQWl4RDc4VDdKcFRmcE09");
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            var services = builder.Services;

            services.AddFluxor(options => {
                options.ScanAssemblies(Assembly.GetExecutingAssembly());
                options.UseReduxDevTools();
            });

            services.AddScoped<StateFacade>();
            services.AddScoped<EntityStateService>();
            services.AddScoped<IEntityStorage, WebApiEntityStorage>();

            var myCloneApiUrl = builder.Configuration["MyClone:Api:Url"];
            var connectionString = builder.Configuration["ConnectionStrings:DatabaseContext"];
            Console.WriteLine($"Middle Tier: [{myCloneApiUrl}]");
            Console.WriteLine($"Database: [{connectionString}]");

            services.AddTransient(sp => new HttpClient { BaseAddress = new Uri("https://localhost:5001") });

            var configuration = services.AddConfigurationWithUserSecrets();
            services.AddLogging();
            services.AddDatabaseContext(connectionString);
            services.AddRepositories();
            services.AddAsanaServices();
            services.AddMediatR();

            builder.Services.AddSyncfusionBlazor();

            await builder.Build().RunAsync();
        }
    }
}


