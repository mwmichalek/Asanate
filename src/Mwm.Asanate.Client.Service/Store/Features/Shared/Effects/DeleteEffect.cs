using Fluxor;
using Microsoft.Extensions.Logging;
using Mwm.Asanate.Client.Service.Storage;
using Mwm.Asanate.Client.Service.Store.Features.Shared.Actions;
using Mwm.Asanate.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Mwm.Asanate.Application.Tsks.Commands;
using Mwm.Asanate.Application.Shared.Commands;

namespace Mwm.Asanate.Client.Service.Store.Features.Shared.Effects {
    public abstract class DeleteEffect<TEntity, TDeleteEntityCommand> : 
                          Effect<DeleteAction<TEntity, TDeleteEntityCommand>> where TEntity : INamedEntity
                                                                              where TDeleteEntityCommand : IDeleteEntityCommand<TEntity> {

        protected readonly ILogger<DeleteEffect<TEntity, TDeleteEntityCommand>> _logger;
        protected readonly IEntityStorage _entityStorage;

        public DeleteEffect(ILogger<DeleteEffect<TEntity, TDeleteEntityCommand>> logger, IEntityStorage entityStorage) =>
            (_logger, _entityStorage) = (logger, entityStorage);

        public override async Task HandleAsync(DeleteAction<TEntity, TDeleteEntityCommand> action, IDispatcher dispatcher) {
            var entityName = typeof(TEntity).Name;
            try {
                _logger.LogInformation($"Deleting {entityName} ...");

                var id = await _entityStorage.Delete<TEntity, TDeleteEntityCommand>(action.EntityCommand);
                _logger.LogInformation($"Delete {entityName} successfully!");
                dispatcher.Dispatch(new DeleteSuccessAction<TEntity>(0));
            } catch (Exception e) {
                _logger.LogError($"Error deleting {entityName}(s), reason: {e}");
                dispatcher.Dispatch(new DeleteFailureAction<TEntity>(e.Message));
            }

        }
    }
}
