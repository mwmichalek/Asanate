using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Mwm.Asana.Model;
using Mwm.Asana.Service.Utils;
using Mwm.MyQ.Application.Asana.Commands;
using Mwm.MyQ.Application.Shared.Commands;
using Mwm.MyQ.Application.Utils;
using Mwm.MyQ.Common.Utils;
using Mwm.MyQ.Data;
using Mwm.MyQ.Data.Utils;
using Mwm.MyQ.Persistance.Shared;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Consol = System.Console;

// Syncfusion Key: NDk3NTY2QDMxMzkyZTMyMmUzMEtidFArTFZsYWhvNGlHcXpaRHkwc0xqZGV3T3EyK0hFWVhHamtzOUVKRVU9

namespace Mwm.MyQ.Console {
    class Program {
        static async Task Main(string[] args) {

            var provider = BuildServiceProvider();
            var mediator = provider.GetService<IMediator>();
            var databaseContext = provider.GetService<DatabaseContext>();

            databaseContext.RecreateDatabase();

            var command = new AsanaEntitiesSynch.Command {
                Since = DateTime.Now.AddDays(-1)
            };

            var result = await mediator.Send(command);

            // Lets see if this triggers a build.
        }

        private static ServiceProvider BuildServiceProvider() {
            var services = new ServiceCollection();

            var configuration = services.AddConfigurationWithUserSecrets();
            //services.AddLogging(opt => {
            //    opt.ClearProviders();
            //    opt.AddConsole();
            //});
            services.AddLogging();
            services.AddDatabaseContext(configuration);
            services.AddRepositories();
            services.AddAsanaServices();
            services.AddMediatR();

            var provider = services.BuildServiceProvider();
            Task.WaitAll(provider.ConfigureAsanaServices());

            return provider;
        }
    }

}






