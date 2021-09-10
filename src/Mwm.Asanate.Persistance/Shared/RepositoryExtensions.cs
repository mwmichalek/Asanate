
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.Asanate.Persistance.Shared {
    public static class RepositoryExtensions {

        public static IServiceCollection AddRepositories(this IServiceCollection services) {
            //services.AddScoped<ICreditGrantRepository, CreditGrantRepository>();
            //services.AddScoped<IMemberRepository, MemberRepository>();
            //services.AddScoped<ICreditTransactionRepository, CreditTransactionRepository>();
            return services;
        }
    }
}
