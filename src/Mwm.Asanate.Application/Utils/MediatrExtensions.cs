using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using System.Threading;
using FluentResults;

namespace Mwm.Asanate.Application.Utils {
    public static class MediatrExtensions {

        public static IServiceCollection AddMediatR(this IServiceCollection services) {
            //var applicationAsm = AppDomain.CurrentDomain.GetAssemblies().Single(alm => alm.FullName.Contains("Application"));

            var applicationAsm = typeof(MediatrExtensions).Assembly;
            services.AddMediatR(new System.Reflection.Assembly[] { applicationAsm });
            return services;
        }

        public static bool TryUsing<TResult>(this Result<TResult> result, out TResult value) where TResult : class {
            if (result.IsSuccess) {
                value = result.Value;
                return true;
            } else {
                value = default;
                return false;
            }
            
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
