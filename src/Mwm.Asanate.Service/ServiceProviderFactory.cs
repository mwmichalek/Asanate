using Microsoft.Extensions.DependencyInjection;
using Mwm.Asanate.Model;
using Mwm.Asanate.Service.AsanaApi;
using Mwm.Asanate.Service.TaskMaster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Mwm.Asanate.Service {
    public class ServiceProviderFactory {

        public static IServiceProvider Build() { 
            var serviceProvider = new ServiceCollection()
            .AddLogging()
            .AddScoped<IAsanaHttpClientFactory, AsanaHttpClientFactory>()
            .AddSingleton<IAsanaService<Tsk>, MemoryCacheAsanaService<Tsk>>()
            .AddSingleton<IAsanaService<Project>, MemoryCacheAsanaService<Project>>()
            .AddSingleton<ISectionAsanaService, SectionMemoryCacheAsanaService>()
            .AddSingleton<ITaskMasterService, TaskMasterService>()
            .BuildServiceProvider();
            return serviceProvider;
        }

    }
}
