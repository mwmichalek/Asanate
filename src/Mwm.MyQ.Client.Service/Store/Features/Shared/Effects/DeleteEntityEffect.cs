using Fluxor;
using Microsoft.Extensions.Logging;
using Mwm.MyQ.Client.Service.Storage;
using Mwm.MyQ.Client.Service.Store.Features.Shared.Actions;
using Mwm.MyQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Mwm.MyQ.Application.Tsks.Commands;
using Mwm.MyQ.Application.Shared.Commands;

namespace Mwm.MyQ.Client.Service.Store.Features.Shared.Effects {
    public abstract class DeleteEntityEffect<TEntity, TDeleteEntityCommand> : 
                          Effect<DeleteEntityAction<TEntity, TDeleteEntityCommand>> where TEntity : INamedEntity
                                                                              where TDeleteEntityCommand : IDeleteEntityCommand<TEntity> {

        protected readonly ILogger<DeleteEntityEffect<TEntity, TDeleteEntityCommand>> _logger;
        protected readonly IEntityStorage _entityStorage;

        public DeleteEntityEffect(ILogger<DeleteEntityEffect<TEntity, TDeleteEntityCommand>> logger, IEntityStorage entityStorage) =>
            (_logger, _entityStorage) = (logger, entityStorage);

        public override async Task HandleAsync(DeleteEntityAction<TEntity, TDeleteEntityCommand> action, IDispatcher dispatcher) {
            var entityName = typeof(TEntity).Name;
            try {
                _logger.LogInformation($"Deleting {entityName} ...");

                var id = await _entityStorage.Delete<TEntity, TDeleteEntityCommand>(action.EntityCommand);
                _logger.LogInformation($"Delete {entityName} successfully!");
                dispatcher.Dispatch(new DeleteEntitySuccessAction<TEntity>(0));

                //NOTE:(MWM) Just seeing if this properly triggers model rebuild.
                //var entities = await _entityStorage.GetAll<TEntity>();
                //dispatcher.Dispatch(new LoadEntitySuccessAction<TEntity>(entities));

            } catch (Exception e) {
                _logger.LogError($"Error deleting {entityName}(s), reason: {e}");
                dispatcher.Dispatch(new DeleteEntityFailureAction<TEntity>(e.Message));
            }

        }
    }
}
