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
    

    public abstract class LoadModelEffect<TEntity, TModel> : Effect<LoadEntitySuccessAction<TEntity>> where TEntity : INamedEntity
                                                                                                      where TModel : INamedEntity {

        protected readonly ILogger<LoadModelEffect<TEntity, TModel>> _logger;

        protected IState<EntityState<TModel>> _entityState { get; set; }

        protected IState<ApplicationState> _applicationState { get; set; }  

        public LoadModelEffect(ILogger<LoadModelEffect<TEntity, TModel>> logger, 
                               IState<EntityState<TModel>> entityState, 
                               IState<ApplicationState> applicationState) => 
                                (_logger, _entityState, _applicationState) = (logger, entityState, applicationState);

        public override Task HandleAsync(LoadEntitySuccessAction<TEntity> action, IDispatcher dispatcher) {
            var entityName = typeof(TEntity).Name;
            try {
                _logger.LogInformation($"Loading models {entityName}(s) ...");

                var models = _entityState.Value.Entities.Select(e => CreateModel(e));
                var filteredModels = models;

                foreach (IModelFilter<TModel> filter in _applicationState.Value.Settings.Where(s => s is IModelFilter<TModel> mf && mf.IsApplied))
                    filteredModels = filter.Filter(filteredModels);

                _logger.LogInformation($"Loaded models {entityName}(s) successfully!");
                dispatcher.Dispatch(new LoadModelSuccessAction<TModel>(models, filteredModels));
            } catch (Exception e) {
                _logger.LogError($"Error loading {entityName}(s), reason: {e}");
                dispatcher.Dispatch(new LoadEntityFailureAction<TModel>(e.Message));
            }
            return Task.CompletedTask;
        }

        public abstract EntityModel<TModel> CreateModel(TModel entity);
    }

    //public abstract class LoadModelEffect<TEntity> : Effect<LoadEntitySuccessAction<TEntity>> where TEntity : INamedEntity {

    //    protected readonly ILogger<LoadModelEffect<TEntity>> _logger;

    //    public LoadModelEffect(ILogger<LoadModelEffect<TEntity>> logger) => (_logger) = (logger);

    //    public override Task HandleAsync(LoadEntitySuccessAction<TEntity> action, IDispatcher dispatcher) {
    //        var entityName = typeof(TEntity).Name;
    //        try {
    //            _logger.LogInformation($"Loading models {entityName}(s) ...");

    //            var models = action.Entities.Select(e => CreateModel(e)).ToList();

    //            //TODO: (MWM) Go through filters and create filteredModels

    //            var filteredModels = models;

    //            _logger.LogInformation($"Loaded models {models.Count} {entityName}(s) successfully!");
    //            dispatcher.Dispatch(new LoadModelSuccessAction<TEntity>(models, filteredModels));
    //        } catch (Exception e) {
    //            _logger.LogError($"Error loading {entityName}(s), reason: {e}");
    //            dispatcher.Dispatch(new LoadEntityFailureAction<TEntity>(e.Message));
    //        }
    //        return Task.CompletedTask;
    //    }

    //    public abstract EntityModel<TEntity> CreateModel(TEntity entity);
    //}
}
