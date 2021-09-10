using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using System.Threading;

namespace Application.Utils {
    public static class MediatrExtensions {

        public static IServiceCollection AddMediatR(this IServiceCollection services) {
            //var applicationAsm = AppDomain.CurrentDomain.GetAssemblies().Single(alm => alm.FullName.Contains("Application"));

            var applicationAsm = typeof(MediatrExtensions).Assembly;
            services.AddMediatR(new System.Reflection.Assembly[] { applicationAsm });
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
