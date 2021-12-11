using Fluxor;
using Microsoft.Extensions.DependencyInjection;
using Mwm.MyQ.Client.Service.Facades;
using Mwm.MyQ.Client.Service.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.MyQ.Client.Service.Utils {
    public static class ClientServiceExtensions {

        public static IServiceCollection AddClientServices(this IServiceCollection services) {
            services.AddFluxor(options => {
                options.ScanAssemblies(Assembly.GetExecutingAssembly());
                options.UseReduxDevTools();
            });

            services.AddScoped<EntityStateFacade>();
            services.AddScoped<IEntityStorage, WebApiEntityStorage>();

            return services;
        }
    }
}
