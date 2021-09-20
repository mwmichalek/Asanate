
using Microsoft.Extensions.DependencyInjection;
using Mwm.Asanate.Application.Interfaces.Persistance;
using Mwm.Asanate.Domain;
using Mwm.Asanate.Persistance.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.Asanate.Persistance.Shared {
    public static class RepositoryExtensions {

        public static IServiceCollection AddRepositories(this IServiceCollection services) {
            services.AddScoped<IRepository<Project>, ProjectRepository>();
            services.AddScoped<IRepository<Company>, Repository<Company>>();
            services.AddScoped<IRepository<Initiative>, InitiativeRepository>();
            services.AddScoped<IRepository<Tsk>, TskRepository>(); // Includes Initiative.Project.Company
            services.AddScoped<IRepository<User>, Repository<User>>();


            //services.AddScoped<IRepository<Workspace>, Repository<Workspace>>();

            return services;
        }
    }
}
