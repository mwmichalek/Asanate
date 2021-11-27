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
    public abstract class LoadEffect<TEntity> : Effect<LoadAction<TEntity>> where TEntity : INamedEntity {

        protected readonly ILogger<LoadEffect<TEntity>> _logger;
        protected readonly IEntityStorage _entityStorage;

        public LoadEffect(ILogger<LoadEffect<TEntity>> logger, IEntityStorage entityStorage) =>
            (_logger, _entityStorage) = (logger, entityStorage);

        public override async Task HandleAsync(LoadAction<TEntity> action, IDispatcher dispatcher) {
            var entityName = typeof(TEntity).Name;
            try {
                _logger.LogInformation($"Loading {entityName}(s) ...");

                // Add a little extra latency for dramatic effect...
                //await Task.Delay(TimeSpan.FromMilliseconds(1000));
                var response = await _entityStorage.GetAll<TEntity>();
                _logger.LogInformation($"Loaded {response.Count} {entityName}(s) successfully!");
                dispatcher.Dispatch(new LoadSuccessAction<TEntity>(response));
            } catch (Exception e) {
                _logger.LogError($"Error loading {entityName}(s), reason: {e}");
                dispatcher.Dispatch(new LoadFailureAction<TEntity>(e.Message));
            }

        }
    }
}
