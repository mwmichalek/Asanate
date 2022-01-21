using Fluxor;
using Microsoft.Extensions.DependencyInjection;
using Mwm.MyQ.Client.Service.Facades;
using Mwm.MyQ.Client.Service.Storage;
using Mwm.MyQ.Domain;
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
            //services.AddScoped<ModelStateFacade>();
            services.AddScoped<ApplicationStateFacade>();
            services.AddScoped<IEntityStorage, WebApiEntityStorage>();

            return services;
        }

        public static async Task UseClientServicesAsync(this IServiceProvider serviceProvider) {
            var entityStateFacade = serviceProvider.GetService<EntityStateFacade>();
            await entityStateFacade.Load<Company>();
            await entityStateFacade.Load<Project>();
            await entityStateFacade.Load<Initiative>();
            await entityStateFacade.Load<Tsk>();
        }
    }
}
