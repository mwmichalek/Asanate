using Fluxor;
using Microsoft.Extensions.Logging;
using Mwm.MyQ.Client.Service.Models;
using Mwm.MyQ.Client.Service.Storage;
using Mwm.MyQ.Client.Service.Store.Features.Settings;
using Mwm.MyQ.Client.Service.Store.Features.Shared.Actions;
using Mwm.MyQ.Client.Service.Store.State.Shared;
using Mwm.MyQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Mwm.MyQ.Client.Service.Store.Features.Shared.Effects {



    public abstract class LoadModelEffect<TModel, TEntity> :
                          Effect<LoadModelAction<TModel, TEntity>> where TModel : EntityModel<TEntity>
                                                                   where TEntity : INamedEntity {

        protected readonly ILogger<LoadModelEffect<TModel, TEntity>> _logger;

        protected IState<ModelState<TModel, TEntity>> _modelState { get; set; }

        public LoadModelEffect(ILogger<LoadModelEffect<TModel, TEntity>> logger,
                               IState<ModelState<TModel, TEntity>> modelState) =>
                                (_logger, _modelState) = (logger, modelState);

        public override Task HandleAsync(LoadModelAction<TModel, TEntity> action, IDispatcher dispatcher) {
            var entityName = typeof(TEntity).Name;
            var modelName = typeof(TModel).Name;

            try {
                var models = action.Entities.Where(e => !e.IsArchived).Select(e => CreateModel(e));
                var filteredModels = Filter(models);

                _logger.LogInformation($">>> Loaded models {modelName}(s), triggered by {entityName}, successfully!");
                dispatcher.Dispatch(new LoadModelSuccessAction<TModel, TEntity>(models, filteredModels));
            } catch (Exception e) {
                _logger.LogError($"Error loading {modelName}(s), reason: {e}");
                dispatcher.Dispatch(new LoadModelFailureAction<TModel, TEntity>(e.Message));
            }
            return Task.CompletedTask;
        }


        public abstract TModel CreateModel(TEntity entity);

        public IEnumerable<TModel> Filter(IEnumerable<TModel> models) {
            var filteredModels = models;
            foreach (var filter in _modelState.Value.ModelFilters.Where(mf => mf.IsApplied))
                filteredModels = filter.Filter(filteredModels);
            return filteredModels;
        }
    }

}
