using Microsoft.Extensions.DependencyInjection;
using Mwm.Asanate.Common.Utils;
using Mwm.Asanate.Data.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.Asanate.Service.Tests {
    public class Startup {

        public void ConfigureServices(IServiceCollection services) {

            //var outputPath = Environment.CurrentDirectory;

            var configuration = services.AddConfigurationWithUserSecrets();

            services.AddLogging();

            services.AddDatabaseContext(configuration);


            //services.AddRepositories();
            //services.AddMediatR();
        }

        public void Configure(IServiceProvider provider) {

        }

    }
}

