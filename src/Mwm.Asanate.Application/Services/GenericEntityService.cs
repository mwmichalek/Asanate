using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using Mwm.Asanate.Application.Interfaces.Persistance;
using Mwm.Asanate.Application.Services;
using Mwm.Asanate.Application.Shared.Commands;
using Mwm.Asanate.Application.Shared.Workflows;
using Mwm.Asanate.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mwm.Asanate.Application.Services {

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

                if (entity == null)
                    await _mediator.Publish(new EntityCommandSuccessEvent<TEntity, IPostEntityCommand<TEntity>>(entity, command));
            }

            return result;
        }
    }
}
