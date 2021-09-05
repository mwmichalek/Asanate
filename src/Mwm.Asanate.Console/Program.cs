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

            var serviceProvider = ServiceProviderFactory.Build();

            var service = serviceProvider.GetService<ITaskMasterService>();
            await service.Test();

        }
    }

}






