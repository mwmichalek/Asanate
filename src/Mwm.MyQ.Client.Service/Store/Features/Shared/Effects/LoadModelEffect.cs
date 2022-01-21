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
    

    public abstract class LoadModelEffect<TModel, TModelEntity, TTriggerEntity> : 
                          Effect<LoadEntitySuccessAction<TTriggerEntity>> where TModel : EntityModel<TModelEntity>
                                                                          where TModelEntity : INamedEntity
                                                                          where TTriggerEntity : INamedEntity {

        protected readonly ILogger<LoadModelEffect<TModel, TModelEntity, TTriggerEntity>> _logger;

        protected IState<EntityState<TModelEntity>> _entityState { get; set; }

        protected IState<ApplicationState> _applicationState { get; set; }  

        public LoadModelEffect(ILogger<LoadModelEffect<TModel, TModelEntity, TTriggerEntity>> logger, 
                               IState<EntityState<TModelEntity>> entityState, 
                               IState<ApplicationState> applicationState) => 
                                (_logger, _entityState, _applicationState) = (logger, entityState, applicationState);

        public override Task HandleAsync(LoadEntitySuccessAction<TTriggerEntity> action, IDispatcher dispatcher) {
            var entityName = typeof(TTriggerEntity).Name;
            var modelName = typeof(TModelEntity).Name;
            try {
                //_logger.LogInformation($"Loading models {entityName}(s) ...");

                var models = _entityState.Value.Entities.Select(e => CreateModel(e));
                var filteredModels = Filter(models);
                
                _logger.LogInformation($"Loaded models {modelName}(s), triggered by {entityName}, successfully!");
                dispatcher.Dispatch(new LoadModelSuccessAction<TModelEntity>(models, filteredModels));
            } catch (Exception e) {
                _logger.LogError($"Error loading {entityName}(s), reason: {e}");
                dispatcher.Dispatch(new LoadEntityFailureAction<TModelEntity>(e.Message));
            }
            return Task.CompletedTask;
        }

        public abstract TModel CreateModel(TModelEntity entity);

        public abstract IEnumerable<EntityModel<TModelEntity>> Filter(IEnumerable<TModel> models); 
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
