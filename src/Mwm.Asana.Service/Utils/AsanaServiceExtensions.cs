using Microsoft.Extensions.DependencyInjection;
using Mwm.Asana.Model;
using Mwm.Asanate.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.Asana.Service.Utils {
    public static class AsanaServiceExtensions {

        public static IServiceCollection AddAsanaServices(this IServiceCollection services, bool delayInit = false) {
            services.AddSingleton<IAsanaHttpClientFactory, AsanaHttpClientFactory>();
            services.AddSingleton<IAsanaService<AsanaTsk>, MemoryCacheAsanaService<AsanaTsk>>();
            services.AddSingleton<IAsanaService<AsanaProject>, MemoryCacheAsanaService<AsanaProject>>();
            services.AddSingleton<IAsanaService<AsanaSection>, SectionMemoryCacheAsanaService>();
            services.AddSingleton<IAsanaService<AsanaUser>, MemoryCacheAsanaService<AsanaUser>>();

            return services;
        }


        public static async Task ConfigureAsanaServices(this IServiceProvider provider) {
            await provider.GetService<IAsanaService<AsanaTsk>>().Initialize();
            await provider.GetService<IAsanaService<AsanaProject>>().Initialize();
            await provider.GetService<IAsanaService<AsanaSection>>().Initialize();
            await provider.GetService<IAsanaService<AsanaUser>>().Initialize();
        }
    }
}






