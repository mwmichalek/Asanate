using Microsoft.Extensions.DependencyInjection;
using Mwm.Asana.Model;
using Mwm.MyQ.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.Asana.Service {
    public static class ServiceRegistry {


        public static void Register(this ServiceCollection serviceCollection) {
            serviceCollection.AddScoped<IAsanaHttpClientFactory, AsanaHttpClientFactory>();
            serviceCollection.AddSingleton<IAsanaService<AsanaTsk>, MemoryCacheAsanaService<AsanaTsk>>();
            serviceCollection.AddSingleton<IAsanaService<AsanaProject>, MemoryCacheAsanaService<AsanaProject>>();
            serviceCollection.AddSingleton<ISectionAsanaService, SectionMemoryCacheAsanaService>();
        }

        //public static IServiceProvider Build() {
        //    var serviceProvider = new ServiceCollection()
        //    .AddLogging()
        //    .AddScoped<IAsanaHttpClientFactory, AsanaHttpClientFactory>()
        //    .AddSingleton<IAsanaService<AsanaTsk>, MemoryCacheAsanaService<AsanaTsk>>()
        //    .AddSingleton<IAsanaService<AsanaProject>, MemoryCacheAsanaService<AsanaProject>>()
        //    .AddSingleton<ISectionAsanaService, SectionMemoryCacheAsanaService>()
        //    //.AddSingleton<ITaskMasterService, TaskMasterService>()
        //    .BuildServiceProvider();
        //    return serviceProvider;
        //}


    }
}
