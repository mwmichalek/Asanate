using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Mwm.Asanate.Common.Utils;
using Mwm.Asanate.Data.Utils;
using Mwm.Asanate.Application.Utils;
using System;
using Mwm.Asanate.Persistance.Shared;
using Mwm.Asana.Service.Utils;
using System.Threading.Tasks;
using System.Net.Http;

namespace Mwm.Asanate.Service.Tests {
    public class Startup {

        public void ConfigureServices(IServiceCollection services) {

            //var outputPath = Environment.CurrentDirectory;

            var configuration = services.AddConfigurationWithUserSecrets();
            services.AddTransient(sp => new HttpClient { BaseAddress = new Uri("https://localhost:5001") });
            services.AddLogging();
            services.AddDatabaseContext(configuration);
            services.AddRepositories();
            services.AddAsanaServices();
            services.AddMediatR(includeAsana:true);
            services.AddMediatR();
        }

        public void Configure(IServiceProvider provider) {
            Task.WaitAll(provider.ConfigureAsanaServices());
        }

    }
}

