using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using Mwm.MyQ.Application.Interfaces.Persistance;
using Mwm.MyQ.Application.Services;
using Mwm.MyQ.Application.Shared.Commands;
using Mwm.MyQ.Application.Shared.Workflows;
using Mwm.MyQ.Application.Tsks.Commands;
using Mwm.MyQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mwm.MyQ.Application.Services {

    public class GenericEntityService<TEntity> : IEntityService<TEntity> where TEntity : INamedEntity {

        public ILogger<GenericEntityService<TEntity>> _logger {  get; set; }     
        public IMediator _mediator {  get; set; }

        public IRepository<TEntity> _repository {  get; set; }

        public GenericEntityService(ILogger<GenericEntityService<TEntity>> logger, IMediator mediator, IRepository<TEntity> repository) {
            _logger = logger;
            _mediator = mediator;
            _repository = repository;
        } 


        public async Task<Result<int>> ExecuteAddCommand(IAddEntityCommand<TEntity> command) {
            return await ExecuteCommandAsync(command);
        }

        public async Task<Result<int>> ExecuteUpdateCommand(IUpdateEntityCommand<TEntity> command) {
            return await ExecuteCommandAsync(command);
        }

        public async Task<Result<int>> ExecuteDeleteCommand(IDeleteEntityCommand<TEntity> command) {
            return await ExecuteCommandAsync(command);
        }

        private async Task<Result<int>> ExecuteCommandAsync(IPostEntityCommand<TEntity> command) {
            var result = await _mediator.Send(command);

            if (result.IsSuccess) {
                var entity = _repository.Get(result.Value);

                if (entity != null)
                    await _mediator.Publish(new EntityCommandSuccessEvent<TEntity>(entity, command));
            }

            return result;
        }
    }

    //public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> {
    //    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    //    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger) {
    //        _logger = logger;
    //    }

    //    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next) {
    //        _logger.LogInformation($">>>>>>>  {typeof(TRequest).Name}");
    //        var response = await next();
    //        _logger.LogInformation($"<<<<<<<<  {typeof(TResponse).Name}");

    //        return response;
    //    }
    //}


    //// https://github.com/jbogard/MediatR.Extensions.Microsoft.DependencyInjection

    //public class EntityCommandPostProcessor<TRequest, TResult> : IRequestPostProcessor<TRequest, TResult> where TRequest : ICommand {

    //    private IMediator _mediator;

    //    public EntityCommandPostProcessor(IMediator mediator) {
    //        _mediator = mediator;
    //    }

    //    public Task Process(TRequest command, TResult response, CancellationToken cancellationToken) {


    //        if (command is TskAdd.Command tskAdd)
    //            Console.WriteLine("NUTS!");
    //        //_mediator.Send()


    //        return Task.CompletedTask;
    //    }
    //}
}
