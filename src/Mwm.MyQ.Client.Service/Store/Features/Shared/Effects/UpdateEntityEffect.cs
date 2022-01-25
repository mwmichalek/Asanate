using Fluxor;
using Microsoft.Extensions.Logging;
using Mwm.MyQ.Application.Shared.Commands;
using Mwm.MyQ.Client.Service.Storage;
using Mwm.MyQ.Client.Service.Store.Features.Shared.Actions;
using Mwm.MyQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Mwm.MyQ.Client.Service.Store.Features.Shared.Effects {
    public abstract class UpdateEntityEffect<TEntity, TUpdateEntityCommand> : 
                          Effect<UpdateEntityAction<TEntity, TUpdateEntityCommand>> where TEntity : INamedEntity
                                                                              where TUpdateEntityCommand : IUpdateEntityCommand<TEntity>{

        protected readonly ILogger<UpdateEntityEffect<TEntity, TUpdateEntityCommand>> _logger;
        protected readonly IEntityStorage _entityStorage;

        public UpdateEntityEffect(ILogger<UpdateEntityEffect<TEntity, TUpdateEntityCommand>> logger, IEntityStorage entityStorage) =>
            (_logger, _entityStorage) = (logger, entityStorage);

        public override async Task HandleAsync(UpdateEntityAction<TEntity, TUpdateEntityCommand> action, IDispatcher dispatcher) {
            var entityName = typeof(TEntity).Name;
            try {
                _logger.LogInformation($"Updating {entityName} ...");

                var id = await _entityStorage.Update<TEntity, TUpdateEntityCommand>(action.EntityCommand);
                _logger.LogInformation($"Updated {entityName} successfully!");

                var entity = await _entityStorage.Get<TEntity>(id);
                _logger.LogInformation($"Retrieved updated {entityName} successfully!");

                dispatcher.Dispatch(new UpdateEntitySuccessAction<TEntity>(entity));

                //NOTE:(MWM) Just seeing if this properly triggers model rebuild.
                var entities = await _entityStorage.GetAll<TEntity>();
                dispatcher.Dispatch(new LoadEntitySuccessAction<TEntity>(entities));

            } catch (Exception e) {
                _logger.LogError($"Error updating {entityName}(s), reason: {e}");
                dispatcher.Dispatch(new UpdateEntityFailureAction<TEntity>(e.Message));
            }

        }

        //public override async Task HandleAsync(AddAction<TEntity, TAddEntityCommand> action, IDispatcher dispatcher) {
        //    var entityName = typeof(TEntity).Name;
        //    try {
        //        _logger.LogInformation($"Adding {entityName} ...");

        //        var id = await _entityStorage.Add<TEntity, IAddEntityCommand<TEntity>>(action.EntityCommand);
        //        _logger.LogInformation($"Added {entityName} successfully!");
        //        dispatcher.Dispatch(new AddSuccessAction<TEntity>(0));
        //    } catch (Exception e) {
        //        _logger.LogError($"Error adding {entityName}(s), reason: {e}");
        //        dispatcher.Dispatch(new AddFailureAction<TEntity>(e.Message));
        //    }

        //}
    }
}
