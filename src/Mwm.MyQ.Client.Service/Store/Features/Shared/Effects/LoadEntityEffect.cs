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

namespace Mwm.MyQ.Client.Service.Store.Features.Shared.Effects {
    public abstract class LoadEntityEffect<TEntity> : Effect<LoadEntityAction<TEntity>> where TEntity : INamedEntity {

        protected readonly ILogger<LoadEntityEffect<TEntity>> _logger;
        protected readonly IEntityStorage _entityStorage;

        public LoadEntityEffect(ILogger<LoadEntityEffect<TEntity>> logger, IEntityStorage entityStorage) =>
            (_logger, _entityStorage) = (logger, entityStorage);

        public override async Task HandleAsync(LoadEntityAction<TEntity> action, IDispatcher dispatcher) {
            var entityName = typeof(TEntity).Name;
            try {
                _logger.LogInformation($"Loading {entityName}(s) ...");

                // Add a little extra latency for dramatic effect...
                //await Task.Delay(TimeSpan.FromMilliseconds(1000));
                var response = await _entityStorage.GetAll<TEntity>();
                _logger.LogInformation($"Loaded {response.Count} {entityName}(s) successfully!");
                dispatcher.Dispatch(new LoadEntitySuccessAction<TEntity>(response));
            } catch (Exception e) {
                _logger.LogError($"Error loading {entityName}(s), reason: {e}");
                dispatcher.Dispatch(new LoadEntityFailureAction<TEntity>(e.Message));
            }

        }
    }
}
