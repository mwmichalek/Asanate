using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Mwm.Asanate.Model;
using Mwm.Asanate.Service;
using Mwm.Asanate.Service.AsanaApi;
using Mwm.Asanate.Service.TaskMaster;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Consol = System.Console;

namespace Mwm.Asanate.Console {
    class Program {
        static async Task Main(string[] args) {

            var serviceProvider = new ServiceCollection()
            .AddLogging()
            .AddSingleton<IAsanaService, AsanaService>()
            .AddSingleton<ITaskMasterService, TaskMasterService>()
            .BuildServiceProvider();

            //serviceProvider
            //    .GetService<ILoggerFactory>().AddCons


            //var logger = serviceProvider.GetService<ILoggerFactory>()
            //    .CreateLogger<Program>();
            //logger.LogDebug("Starting application");

            //Consol.WriteLine("Hello World!");


            //var asanateService = serviceProvider.GetService<IAsanaService>();

            //var projects = await asanateService.GetAll<Project>();

            var service = serviceProvider.GetService<ITaskMasterService>();
            await service.Test();

        }
    }

}






