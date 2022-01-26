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

    //TODO:(MWM) Create concrete instance triggered by each entity type

    public class InitiativeLoadTriggersLoadTskModelEffect : EntityLoadTriggersLoadModelEffect<TskModel, Tsk, Initiative> {
        public InitiativeLoadTriggersLoadTskModelEffect(ILogger<LoadModelEffect<TskModel, Tsk, Initiative>> logger, IState<EntityState<Tsk>> entityState) : base(logger, entityState) {
        }
    }

    public class ProjectLoadTriggersLoadTskModelEffect : EntityLoadTriggersLoadModelEffect<TskModel, Tsk, Project> {
        public ProjectLoadTriggersLoadTskModelEffect(ILogger<LoadModelEffect<TskModel, Tsk, Project>> logger, IState<EntityState<Tsk>> entityState) : base(logger, entityState) {
        }
    }

    public class CompanyLoadTriggersLoadTskModelEffect : EntityLoadTriggersLoadModelEffect<TskModel, Tsk, Company> {
        public CompanyLoadTriggersLoadTskModelEffect(ILogger<LoadModelEffect<TskModel, Tsk, Company>> logger, IState<EntityState<Tsk>> entityState) : base(logger, entityState) {
        }
    }

    public class TskAddTriggersLoadTskModelEffect : EntityAddTriggersLoadModelEffect<TskModel, Tsk, Tsk> {
        public TskAddTriggersLoadTskModelEffect(ILogger<EntityAddTriggersLoadModelEffect<TskModel, Tsk, Tsk>> logger, IState<EntityState<Tsk>> entityState) : base(logger, entityState) {
        }
    }

    public class TskUpdateTriggersLoadTskModelEffect : EntityUpdateTriggersLoadModelEffect<TskModel, Tsk, Tsk> {
        public TskUpdateTriggersLoadTskModelEffect(ILogger<EntityUpdateTriggersLoadModelEffect<TskModel, Tsk, Tsk>> logger, IState<EntityState<Tsk>> entityState) : base(logger, entityState) {
        }
    }

    public class TskDeleteTriggersLoadTskModelEffect : EntityDeleteTriggersLoadModelEffect<TskModel, Tsk, Tsk> {
        public TskDeleteTriggersLoadTskModelEffect(ILogger<EntityDeleteTriggersLoadModelEffect<TskModel, Tsk, Tsk>> logger, IState<EntityState<Tsk>> entityState) : base(logger, entityState) {
        }
    }


    public abstract class EntityLoadTriggersLoadModelEffect<TModel, TEntity, TTriggerEntity> :
                          Effect<LoadEntitySuccessAction<TTriggerEntity>> where TModel : EntityModel<TEntity>
                                                                          where TEntity : INamedEntity
                                                                          where TTriggerEntity : INamedEntity {

        protected readonly ILogger<LoadModelEffect<TModel, TEntity, TTriggerEntity>> _logger;

        protected IState<EntityState<TEntity>> _entityState { get; set; }

        public EntityLoadTriggersLoadModelEffect(ILogger<LoadModelEffect<TModel, TEntity, TTriggerEntity>> logger, IState<EntityState<TEntity>> entityState) =>
                                (_logger, _entityState) = (logger, entityState);

        public override Task HandleAsync(LoadEntitySuccessAction<TTriggerEntity> action, IDispatcher dispatcher) {
            dispatcher.Dispatch(new LoadModelAction<TModel, TEntity>(_entityState.Value.Entities));
            return Task.CompletedTask;
        }
    }

    public abstract class EntityAddTriggersLoadModelEffect<TModel, TEntity, TTriggerEntity> :
                          Effect<AddEntitySuccessAction<TTriggerEntity>> where TModel : EntityModel<TEntity>
                                                                          where TEntity : INamedEntity
                                                                          where TTriggerEntity : INamedEntity {

        protected readonly ILogger<EntityAddTriggersLoadModelEffect<TModel, TEntity, TTriggerEntity>> _logger;

        protected IState<EntityState<TEntity>> _entityState { get; set; }

        public EntityAddTriggersLoadModelEffect(ILogger<EntityAddTriggersLoadModelEffect<TModel, TEntity, TTriggerEntity>> logger, IState<EntityState<TEntity>> entityState) =>
                                (_logger, _entityState) = (logger, entityState);

        public override Task HandleAsync(AddEntitySuccessAction<TTriggerEntity> action, IDispatcher dispatcher) {
            dispatcher.Dispatch(new LoadModelAction<TModel, TEntity>(_entityState.Value.Entities));
            return Task.CompletedTask;
        }
    }

    public abstract class EntityUpdateTriggersLoadModelEffect<TModel, TEntity, TTriggerEntity> :
                          Effect<UpdateEntitySuccessAction<TTriggerEntity>> where TModel : EntityModel<TEntity>
                                                                          where TEntity : INamedEntity
                                                                          where TTriggerEntity : INamedEntity {

        protected readonly ILogger<EntityUpdateTriggersLoadModelEffect<TModel, TEntity, TTriggerEntity>> _logger;

        protected IState<EntityState<TEntity>> _entityState { get; set; }

        public EntityUpdateTriggersLoadModelEffect(ILogger<EntityUpdateTriggersLoadModelEffect<TModel, TEntity, TTriggerEntity>> logger, IState<EntityState<TEntity>> entityState) =>
                                (_logger, _entityState) = (logger, entityState);

        public override Task HandleAsync(UpdateEntitySuccessAction<TTriggerEntity> action, IDispatcher dispatcher) {
            dispatcher.Dispatch(new LoadModelAction<TModel, TEntity>(_entityState.Value.Entities));
            return Task.CompletedTask;
        }
    }

    public abstract class EntityDeleteTriggersLoadModelEffect<TModel, TEntity, TTriggerEntity> :
                          Effect<DeleteEntitySuccessAction<TTriggerEntity>> where TModel : EntityModel<TEntity>
                                                                          where TEntity : INamedEntity
                                                                          where TTriggerEntity : INamedEntity {

        protected readonly ILogger<EntityDeleteTriggersLoadModelEffect<TModel, TEntity, TTriggerEntity>> _logger;

        protected IState<EntityState<TEntity>> _entityState { get; set; }

        public EntityDeleteTriggersLoadModelEffect(ILogger<EntityDeleteTriggersLoadModelEffect<TModel, TEntity, TTriggerEntity>> logger, IState<EntityState<TEntity>> entityState) =>
                                (_logger, _entityState) = (logger, entityState);

        public override Task HandleAsync(DeleteEntitySuccessAction<TTriggerEntity> action, IDispatcher dispatcher) {
            dispatcher.Dispatch(new LoadModelAction<TModel, TEntity>(_entityState.Value.Entities));
            return Task.CompletedTask;
        }
    }




    public abstract class LoadModelEffect<TModel, TEntity> :
                          Effect<LoadModelAction<TModel, TEntity>> where TModel : EntityModel<TEntity>
                                                                   where TEntity : INamedEntity {

        protected readonly ILogger<LoadModelEffect<TModel, TEntity>> _logger;

        protected IState<ApplicationState> _applicationState { get; set; }

        public LoadModelEffect(ILogger<LoadModelEffect<TModel, TEntity>> logger,
                               IState<ApplicationState> applicationState) =>
                                (_logger, _applicationState) = (logger, applicationState);

        public override Task HandleAsync(LoadModelAction<TModel, TEntity> action, IDispatcher dispatcher) {
            var entityName = typeof(TEntity).Name;
            var modelName = typeof(TModel).Name;

            try {
                var models = action.Entities.Where(e => !e.IsArchived).Select(e => CreateModel(e));
                var filteredModels = Filter(models);

                _logger.LogInformation($"Loaded models {modelName}(s), triggered by {entityName}, successfully!");
                dispatcher.Dispatch(new LoadModelSuccessAction<TModel, TEntity>(models, filteredModels));
            } catch (Exception e) {
                _logger.LogError($"Error loading {modelName}(s), reason: {e}");
                dispatcher.Dispatch(new LoadModelFailureAction<TModel, TEntity>(e.Message));
            }
            return Task.CompletedTask;
        }


        public abstract TModel CreateModel(TEntity entity);

        public abstract IEnumerable<TModel> Filter(IEnumerable<TModel> models);
    }

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
                var models = _entityState.Value.Entities.Where(e => !e.IsArchived).Select(e => CreateModel(e));
                var filteredModels = Filter(models);
                
                _logger.LogInformation($"Loaded models {modelName}(s), triggered by {entityName}, successfully!");
                dispatcher.Dispatch(new LoadModelSuccessAction<TModel, TModelEntity>(models, filteredModels));
            } catch (Exception e) {
                _logger.LogError($"Error loading {entityName}(s), reason: {e}");
                dispatcher.Dispatch(new LoadEntityFailureAction<TModelEntity>(e.Message));
            }
            return Task.CompletedTask;
        }

        public abstract TModel CreateModel(TModelEntity entity);

        public abstract IEnumerable<TModel> Filter(IEnumerable<TModel> models); 
    }



   
    //public abstract class LoadModelBecauseOfAdditionEffect<TModel, TModelEntity, TTriggerEntity> :
    //                      Effect<LoadEntitySuccessAction<TTriggerEntity>> where TModel : EntityModel<TModelEntity>
    //                                                                      where TModelEntity : INamedEntity
    //                                                                      where TTriggerEntity : INamedEntity {

    //    protected readonly ILogger<LoadModelEffect<TModel, TModelEntity, TTriggerEntity>> _logger;

    //    protected IState<EntityState<TModelEntity>> _entityState { get; set; }

    //    protected IState<ApplicationState> _applicationState { get; set; }

    //    public LoadModelEffect(ILogger<LoadModelEffect<TModel, TModelEntity, TTriggerEntity>> logger,
    //                           IState<EntityState<TModelEntity>> entityState,
    //                           IState<ApplicationState> applicationState) =>
    //                            (_logger, _entityState, _applicationState) = (logger, entityState, applicationState);

    //    public override Task HandleAsync(LoadEntitySuccessAction<TTriggerEntity> action, IDispatcher dispatcher) {
    //        var entityName = typeof(TTriggerEntity).Name;
    //        var modelName = typeof(TModelEntity).Name;
    //        try {
    //            var models = _entityState.Value.Entities.Where(e => !e.IsArchived).Select(e => CreateModel(e));
    //            var filteredModels = Filter(models);

    //            _logger.LogInformation($"Loaded models {modelName}(s), triggered by {entityName}, successfully!");
    //            dispatcher.Dispatch(new LoadModelSuccessAction<TModel, TModelEntity>(models, filteredModels));
    //        } catch (Exception e) {
    //            _logger.LogError($"Error loading {entityName}(s), reason: {e}");
    //            dispatcher.Dispatch(new LoadEntityFailureAction<TModelEntity>(e.Message));
    //        }
    //        return Task.CompletedTask;
    //    }

   
    //}

}
