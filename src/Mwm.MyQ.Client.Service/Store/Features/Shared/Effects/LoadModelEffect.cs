using Fluxor;
using Microsoft.Extensions.Logging;
using Mwm.MyQ.Client.Service.Models;
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
    public abstract class LoadModelEffect<TEntity> : Effect<LoadModelAction<TEntity>> where TEntity : INamedEntity {

        protected readonly ILogger<LoadEntityEffect<TEntity>> _logger;
        
        public LoadModelEffect(ILogger<LoadEntityEffect<TEntity>> logger) =>
            (_logger) = (logger);

        public override async Task HandleAsync(LoadModelAction<TEntity> action, IDispatcher dispatcher) {
            var entityName = typeof(TEntity).Name;
            try {
                _logger.LogInformation($"Loading models {entityName}(s) ...");

                //TODO: (MWM) Create Models here.
                var models = new List<EntityModel<TEntity>>();

                _logger.LogInformation($"Loaded models {models.Count} {entityName}(s) successfully!");
                dispatcher.Dispatch(new LoadModelSuccessAction<TEntity>(models));
            } catch (Exception e) {
                _logger.LogError($"Error loading {entityName}(s), reason: {e}");
                dispatcher.Dispatch(new LoadEntityFailureAction<TEntity>(e.Message));
            }

        }
    }
}
