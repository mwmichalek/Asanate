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
    public abstract class AddEffect<TEntity, TAddEntityCommand> : 
                          Effect<AddAction<TEntity, TAddEntityCommand>> where TEntity : INamedEntity
                                                                        where TAddEntityCommand : IAddEntityCommand<TEntity> {

        protected readonly ILogger<AddEffect<TEntity, TAddEntityCommand>> _logger;
        protected readonly IEntityStorage _entityStorage;

        public AddEffect(ILogger<AddEffect<TEntity, TAddEntityCommand>> logger, IEntityStorage entityStorage) =>
            (_logger, _entityStorage) = (logger, entityStorage);

        public override async Task HandleAsync(AddAction<TEntity, TAddEntityCommand> action, IDispatcher dispatcher) {
            var entityName = typeof(TEntity).Name;
            try {
                _logger.LogInformation($"Adding {entityName} ...");

                var id = await _entityStorage.Add<TEntity, TAddEntityCommand>(action.EntityCommand);
                _logger.LogInformation($"Added {entityName} successfully!");

                var entity = await _entityStorage.Get<TEntity>(id);
                _logger.LogInformation($"Retrieved added {entityName} successfully!");

                dispatcher.Dispatch(new AddSuccessAction<TEntity>(entity));
            } catch (Exception e) {
                _logger.LogError($"Error adding {entityName}(s), reason: {e}");
                dispatcher.Dispatch(new AddFailureAction<TEntity>(e.Message));
            }

        }
    }
}
