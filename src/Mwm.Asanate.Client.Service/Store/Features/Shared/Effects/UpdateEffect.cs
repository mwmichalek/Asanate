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

namespace Mwm.Asanate.Client.Service.Store.Features.Shared.Effects {
    public abstract class UpdateEffect<TEntity> : Effect<UpdateAction<TEntity>> where TEntity : INamedEntity {

        protected readonly ILogger<UpdateEffect<TEntity>> _logger;
        protected readonly IEntityStorage _entityStorage;

        public UpdateEffect(ILogger<UpdateEffect<TEntity>> logger, IEntityStorage entityStorage) =>
            (_logger, _entityStorage) = (logger, entityStorage);

        public override async Task HandleAsync(UpdateAction<TEntity> action, IDispatcher dispatcher) {
            var entityName = typeof(TEntity).Name;
            try {
                _logger.LogInformation($"Updating {entityName} ...");

                var id = await _entityStorage.Update(action.Entity);
                _logger.LogInformation($"Updated {entityName} successfully!");
                dispatcher.Dispatch(new UpdateSuccessAction<TEntity>(id));
            } catch (Exception e) {
                _logger.LogError($"Error updating {entityName}(s), reason: {e}");
                dispatcher.Dispatch(new UpdateFailureAction<TEntity>(e.Message));
            }

        }
    }
}
