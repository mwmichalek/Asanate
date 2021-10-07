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

namespace Mwm.Asanate.Application.Utils {
    public static class MediatrExtensions {

        public static IServiceCollection AddMediatR(this IServiceCollection services, bool includeAsana = false) {
            var asms = new List<System.Reflection.Assembly>();

            asms.Add(AppDomain.CurrentDomain.GetAssemblies().Single(alm => alm.GetName().Name == "Mwm.Asanate.Application"));
            if (includeAsana)
                asms.Add(AppDomain.CurrentDomain.GetAssemblies().Single(alm => alm.GetName().Name == "Mwm.Asanate.Application.Asanate"));

            //var applicationAsm = typeof(MediatrExtensions).Assembly;
            services.AddMediatR(asms.ToArray());
            return services;
        }

    }

    public abstract class AsyncRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse> {
        async Task<TResponse> IRequestHandler<TRequest, TResponse>.Handle(TRequest request, CancellationToken cancellationToken) {
            var response = await Handle(request, cancellationToken);
            return response;
        }

        /// <summary>
        /// Override in a derived class for the handler logic
        /// </summary>
        /// <param name="request">Request</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Response</returns>
        protected abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);

    }

}
