using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using System.Threading;
using FluentResults;
using Mwm.MyQ.Domain;
using Mwm.MyQ.Application.Tsks.Commands;
using MediatR.Pipeline;
using Mwm.MyQ.Application.Shared.Commands;
using Mwm.MyQ.Application.Services;
using Mwm.MyQ.Application.Shared.Workflows;

namespace Mwm.MyQ.Application.Utils {
    public static class MediatrExtensions {

        public static IServiceCollection AddMediatR(this IServiceCollection services, bool includeAsana = false) {


            services.AddTransient(typeof(IEntityService<>), typeof(GenericEntityService<>));

            var asms = new List<System.Reflection.Assembly>();

            asms.Add(AppDomain.CurrentDomain.GetAssemblies().Single(alm => alm.GetName().Name == "Mwm.MyQ.Application"));
            if (includeAsana)
                asms.Add(AppDomain.CurrentDomain.GetAssemblies().Single(alm => alm.GetName().Name == "Mwm.MyQ.Application.Asana"));

            services.AddMediatR(asms.ToArray());

            return services;
        }

    }

}


//services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
//services.AddTransient(typeof(IRequestPostProcessor<IEntityCommand<>, Result>), typeof(EntityCommandPostProcessor<>));
//services.AddTransient(typeof(IRequestPostProcessor<,>), typeof(EntityCommandPostProcessor<,>));
//services.AddTransient(typeof(INotificationHandler<EntityCommandSuccessEvent<>>), )
//services.AddTransient<INotificationHandler<EntityCommandSuccessEvent<Tsk, IAddEntityCommand<Tsk>>>, TestListener>();
