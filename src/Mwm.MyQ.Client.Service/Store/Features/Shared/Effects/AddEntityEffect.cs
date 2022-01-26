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
    public abstract class AddEntityEffect<TEntity, TAddEntityCommand> : 
                          Effect<AddEntityAction<TEntity, TAddEntityCommand>> where TEntity : INamedEntity
                                                                        where TAddEntityCommand : IAddEntityCommand<TEntity> {

        protected readonly ILogger<AddEntityEffect<TEntity, TAddEntityCommand>> _logger;
        protected readonly IEntityStorage _entityStorage;

        public AddEntityEffect(ILogger<AddEntityEffect<TEntity, TAddEntityCommand>> logger, IEntityStorage entityStorage) =>
            (_logger, _entityStorage) = (logger, entityStorage);

        public override async Task HandleAsync(AddEntityAction<TEntity, TAddEntityCommand> action, IDispatcher dispatcher) {
            var entityName = typeof(TEntity).Name;
            try {
                _logger.LogInformation($"Adding {entityName} ...");

                var id = await _entityStorage.Add<TEntity, TAddEntityCommand>(action.EntityCommand);
                _logger.LogInformation($"Added {entityName} successfully!");

                var entity = await _entityStorage.Get<TEntity>(id);
                _logger.LogInformation($"Retrieved added {entityName} successfully!");
                dispatcher.Dispatch(new AddEntitySuccessAction<TEntity>(entity));

                //NOTE:(MWM) Just seeing if this properly triggers model rebuild.
                //var entities = await _entityStorage.GetAll<TEntity>();
                //dispatcher.Dispatch(new LoadEntitySuccessAction<TEntity>(entities));

            } catch (Exception e) {
                _logger.LogError($"Error adding {entityName}(s), reason: {e}");
                dispatcher.Dispatch(new AddEntityFailureAction<TEntity>(e.Message));
            }

        }
    }
}
