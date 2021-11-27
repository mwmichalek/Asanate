using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Mwm.MyQ.Service.Tests.Fixtures {
    public class ServiceProviderFixture : IDisposable {

        public IServiceProvider ServiceProvider { get; private set; }

        public ServiceProviderFixture() {
            ServiceProvider = ServiceProviderFactory.Build();
        }

        public void Dispose() {
        }
    }

    [CollectionDefinition("ServiceProvider Collection")]
    public class ServiceProviderCollection : ICollectionFixture<ServiceProviderFixture> {

    }
}
