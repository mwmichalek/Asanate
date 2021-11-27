using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mwm.MyQ.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Mwm.MyQ.Client.Tests.Fixtures {
    public class ServerFixture : IDisposable {

        public ServerFixture() {
            Task.Run(() => Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(webBuilder => {
                    webBuilder.UseStartup<Mwm.MyQ.Server.Startup>()
                              .UseUrls("http://*:5000", "https://*:5001");
                }).Build().Run());
        }

        public void Dispose() {
        }
    }

    [CollectionDefinition("WebApi")]
    public class ControllerCollection : ICollectionFixture<ServerFixture> {

    }
}
