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

    public abstract class FilterModelEffect<TModel, TEntity> :
                          Effect<FilterModelAction<TModel, TEntity>> where TModel : EntityModel<TEntity>
                                                                     where TEntity : INamedEntity {

        protected readonly ILogger<FilterModelEffect<TModel, TEntity>> _logger;

        protected IState<ModelState<TModel, TEntity>> _modelState { get; set; }

        public FilterModelEffect(ILogger<FilterModelEffect<TModel, TEntity>> logger,
                                 IState<ModelState<TModel, TEntity>> modelState) =>
                                 (_logger, _modelState) = (logger, modelState);

        public override Task HandleAsync(FilterModelAction<TModel, TEntity> action, IDispatcher dispatcher) {
            var entityName = typeof(TEntity).Name;
            var modelName = typeof(TModel).Name;

            try {
                var models = _modelState.Value.Models;

                var modelFilters = _modelState.Value.ModelFilters.ToList();
                var newFilter = action.ModelFilter;
                var oldFilter = modelFilters.SingleOrDefault(mf => mf.GetType() == newFilter.GetType());

                if (oldFilter != null)
                    modelFilters.Remove(oldFilter);
                modelFilters.Add(oldFilter);

                var filteredModels = Filter(models);

                _logger.LogInformation($">>> Loaded models {modelName}(s), triggered by {entityName}, successfully!");
                dispatcher.Dispatch(new FilterModelSuccessAction<TModel, TEntity>(action.ModelFilter, modelFilters, filteredModels));
            } catch (Exception e) {
                _logger.LogError($"Error loading {modelName}(s), reason: {e}");
                dispatcher.Dispatch(new FilterModelFailureAction<TModel, TEntity>(e.Message));
            }
            return Task.CompletedTask;
        }

        public IEnumerable<TModel> Filter(IEnumerable<TModel> models) {
            var filteredModels = models;
            foreach (var filter in _modelState.Value.ModelFilters.Where(mf => mf.IsApplied))
                filteredModels = filter.Filter(filteredModels);
            return filteredModels;
        }
    }

}
