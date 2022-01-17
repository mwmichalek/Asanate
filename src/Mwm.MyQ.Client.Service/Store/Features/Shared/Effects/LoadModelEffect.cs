using Fluxor;
using Microsoft.Extensions.Logging;
using Mwm.MyQ.Client.Service.Models;
using Mwm.MyQ.Client.Service.Storage;
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
    public abstract class LoadModelEffect<TEntity> : Effect<LoadModelAction<TEntity>> where TEntity : INamedEntity {

        protected readonly ILogger<LoadEntityEffect<TEntity>> _logger;

        //protected IState<EntityState<Tsk>> _tsksState { get; set; }

        //protected IState<EntityState<Initiative>> _initiativesState { get; set; }

        //protected IState<EntityState<Project>> _projectsState { get; set; }

        //protected IState<EntityState<Company>> _companiesState { get; set; }

        //public LoadModelEffect(ILogger<LoadEntityEffect<TEntity>> logger,
        //                       IState<EntityState<Tsk>> tsksState,
        //                       IState<EntityState<Initiative>> initiativesState,
        //                       IState<EntityState<Project>> projectsState,
        //                       IState<EntityState<Company>> companiesState) =>
        //                       (_logger, _tsksState, _initiativesState, _projectsState, _companiesState) = 
        //                       (logger, tsksState, initiativesState, projectsState, companiesState);

        public LoadModelEffect(ILogger<LoadEntityEffect<TEntity>> logger) => (_logger) = (logger);

        public override async Task HandleAsync(LoadModelAction<TEntity> action, IDispatcher dispatcher) {
            var entityName = typeof(TEntity).Name;
            try {
                _logger.LogInformation($"Loading models {entityName}(s) ...");

                var models = action.Entities.Select(e => CreateModel(e)).ToList();

                _logger.LogInformation($"Loaded models {models.Count} {entityName}(s) successfully!");
                dispatcher.Dispatch(new LoadModelSuccessAction<TEntity>(models));
            } catch (Exception e) {
                _logger.LogError($"Error loading {entityName}(s), reason: {e}");
                dispatcher.Dispatch(new LoadEntityFailureAction<TEntity>(e.Message));
            }
        }

        public abstract EntityModel<TEntity> CreateModel(TEntity entity);
    }
}
