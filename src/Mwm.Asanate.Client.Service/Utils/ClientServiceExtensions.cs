using Fluxor;
using Microsoft.Extensions.DependencyInjection;
using Mwm.Asanate.Client.Service.Facades;
using Mwm.Asanate.Client.Service.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.Asanate.Client.Service.Utils {
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
