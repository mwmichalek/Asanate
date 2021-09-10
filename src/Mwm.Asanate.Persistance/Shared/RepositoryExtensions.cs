
using Microsoft.Extensions.DependencyInjection;
using Mwm.Asanate.Application.Interfaces.Persistance;
using Mwm.Asanate.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.Asanate.Persistance.Shared {
    public static class RepositoryExtensions {

        public static IServiceCollection AddRepositories(this IServiceCollection services) {
            services.AddScoped<IRepository<Project>, Repository<Project>>();
            services.AddScoped<IRepository<Company>, Repository<Company>>();
            services.AddScoped<IRepository<Initiative>, Repository<Initiative>>();
            services.AddScoped<IRepository<Tsk>, Repository<Tsk>>();
            services.AddScoped<IRepository<User>, Repository<User>>();
            services.AddScoped<IRepository<Project>, Repository<Project>>();
            //services.AddScoped<IRepository<Workspace>, Repository<Workspace>>();

            return services;
        }
    }
}
