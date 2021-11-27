using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Mwm.MyQ.Common.Utils;
using Mwm.MyQ.Data.Utils;
using Mwm.MyQ.Application.Utils;
using System;
using Mwm.MyQ.Persistance.Shared;
//using Mwm.Asana.Service.Utils;
using System.Threading.Tasks;
using System.Net.Http;
using Mwm.MyQ.Client.Service.Utils;

namespace Mwm.MyQ.Client.Tests {
    public class Startup {

        public void ConfigureServices(IServiceCollection services) {

            //var outputPath = Environment.CurrentDirectory;

            var configuration = services.AddConfigurationWithUserSecrets();
            services.AddTransient(sp => new HttpClient { BaseAddress = new Uri("https://localhost:5001") });
            services.AddLogging();
            services.AddDatabaseContext(configuration);
            services.AddClientServices();
        }

        public void Configure(IServiceProvider provider) {
            //Task.WaitAll(provider.ConfigureAsanaServices());
        }

    }
}

