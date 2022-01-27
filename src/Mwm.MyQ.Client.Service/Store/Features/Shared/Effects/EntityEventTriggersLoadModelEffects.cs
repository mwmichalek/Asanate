using Fluxor;
using Microsoft.Extensions.Logging;
using Mwm.MyQ.Client.Service.Models;
using Mwm.MyQ.Client.Service.Store.Features.Shared.Actions;
using Mwm.MyQ.Client.Service.Store.State.Shared;
using Mwm.MyQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.MyQ.Client.Service.Store.Features.Shared.Effects;

public abstract class EntityLoadTriggersLoadModelEffect<TModel, TEntity, TTriggerEntity> :
                          Effect<LoadEntitySuccessAction<TTriggerEntity>> where TModel : EntityModel<TEntity>
                                                                          where TEntity : INamedEntity
                                                                          where TTriggerEntity : INamedEntity {

    protected readonly ILogger<EntityLoadTriggersLoadModelEffect<TModel, TEntity, TTriggerEntity>> _logger;

    protected IState<EntityState<TEntity>> _entityState { get; set; }

    public EntityLoadTriggersLoadModelEffect(ILogger<EntityLoadTriggersLoadModelEffect<TModel, TEntity, TTriggerEntity>> logger, IState<EntityState<TEntity>> entityState) =>
                            (_logger, _entityState) = (logger, entityState);

    public override Task HandleAsync(LoadEntitySuccessAction<TTriggerEntity> action, IDispatcher dispatcher) {
        _logger.LogInformation($"Successfully Loaded {typeof(TEntity).Name}, triggering model {typeof(TModel).Name} Load.");
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
        _logger.LogInformation($"Successfully Added {typeof(TEntity).Name}, triggering model {typeof(TModel).Name} Load.");
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
        _logger.LogInformation($"Successfully Updated {typeof(TEntity).Name}, triggering model {typeof(TModel).Name} Load.");
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
        _logger.LogInformation($"Successfully Deleted {typeof(TEntity).Name}, triggering model {typeof(TModel).Name} Load.");
        dispatcher.Dispatch(new LoadModelAction<TModel, TEntity>(_entityState.Value.Entities));
        return Task.CompletedTask;
    }
}
