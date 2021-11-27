using Microsoft.Extensions.DependencyInjection;
using Mwm.Asana.Model;
//using Mwm.Asana.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Mwm.Asana.Service;

namespace Mwm.MyQ.Service {
    public class ServiceProviderFactory {

        public static IServiceProvider Build() {
            var serviceProvider = new ServiceCollection()
            .AddLogging()
            .AddScoped<IAsanaHttpClientFactory, AsanaHttpClientFactory>()
            .AddSingleton<IAsanaService<AsanaTsk>, MemoryCacheAsanaService<AsanaTsk>>()
            .AddSingleton<IAsanaService<AsanaProject>, MemoryCacheAsanaService<AsanaProject>>()
            .AddSingleton<ISectionAsanaService, SectionMemoryCacheAsanaService>()
            .BuildServiceProvider();
            return serviceProvider;
        }

    }
}
