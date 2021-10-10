using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using System.Threading;
using FluentResults;
using Mwm.Asanate.Domain;
using Mwm.Asanate.Application.Tsks.Commands;
using MediatR.Pipeline;
using Mwm.Asanate.Application.Shared.Commands;
using Mwm.Asanate.Application.Services;

namespace Mwm.Asanate.Application.Utils {
    public static class MediatrExtensions {

        public static IServiceCollection AddMediatR(this IServiceCollection services, bool includeAsana = false) {
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            //services.AddTransient(typeof(IRequestPostProcessor<IEntityCommand<>, Result>), typeof(EntityCommandPostProcessor<>));
            //services.AddTransient(typeof(IRequestPostProcessor<,>), typeof(EntityCommandPostProcessor<,>));

            services.AddTransient(typeof(IEntityService<>), typeof(GenericEntityService<>));

            var asms = new List<System.Reflection.Assembly>();

            asms.Add(AppDomain.CurrentDomain.GetAssemblies().Single(alm => alm.GetName().Name == "Mwm.Asanate.Application"));
            if (includeAsana)
                asms.Add(AppDomain.CurrentDomain.GetAssemblies().Single(alm => alm.GetName().Name == "Mwm.Asanate.Application.Asanate"));

            //var applicationAsm = typeof(MediatrExtensions).Assembly;
            services.AddMediatR(asms.ToArray());
            return services;
        }

    }

}
