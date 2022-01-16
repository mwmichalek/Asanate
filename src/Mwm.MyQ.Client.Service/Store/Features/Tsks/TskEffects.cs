using Microsoft.Extensions.Logging;
using Mwm.MyQ.Client.Service.Storage;
using Mwm.MyQ.Client.Service.Store.Features.Shared.Effects;
using Mwm.MyQ.Domain;
using Mwm.MyQ.Application.Tsks.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Mwm.MyQ.Client.Service.Store.Features.Shared.Actions;
using Fluxor;
using Mwm.MyQ.Client.Service.Store.State.Shared;
using Mwm.MyQ.Client.Service.Store.Features.Shared.Helpers;

namespace Mwm.MyQ.Client.Service.Store.Features.Tsks {
    public class TskLoadEffect : LoadEffect<Tsk> {

        public TskLoadEffect(ILogger<LoadEffect<Tsk>> logger, IEntityStorage entityStorage) : base(logger, entityStorage) { }

    }

    public class TskAddEffect : AddEffect<Tsk, TskAdd.Command> {

        public TskAddEffect(ILogger<AddEffect<Tsk, TskAdd.Command>> logger, IEntityStorage entityStorage) : base(logger, entityStorage) { }

    }

    public class TskUpdateEffect : UpdateEffect<Tsk, TskUpdate.Command> {

        public TskUpdateEffect(ILogger<UpdateEffect<Tsk, TskUpdate.Command>> logger, IEntityStorage entityStorage) : base(logger, entityStorage) { }

    }

    public class TskDeleteEffect : DeleteEffect<Tsk, TskDelete.Command> {

        public TskDeleteEffect(ILogger<DeleteEffect<Tsk, TskDelete.Command>> logger, IEntityStorage entityStorage) : base(logger, entityStorage) { }

    }

    public class TskModelBuilderEffect : Effect<LoadSuccessAction<Tsk>> {

        protected readonly ILogger<TskModelBuilderEffect> _logger;

        private IState<EntityState<Tsk>> _tsksState { get; set; }

        private IState<EntityState<Initiative>> _initiativesState { get; set; }

        private IState<EntityState<Project>> _projectsState { get; set; }

        private IState<EntityState<Company>> _companiesState { get; set; }

        public TskModelBuilderEffect(ILogger<TskModelBuilderEffect> logger, 
                                     IState<EntityState<Tsk>> tsksState,
                                     IState<EntityState<Initiative>> initiativesState,
                                     IState<EntityState<Project>> projectsState,
                                     IState<EntityState<Company>> companiesState) { 
            _logger = logger;
            _tsksState = tsksState;
            _initiativesState = initiativesState;
            _projectsState = projectsState;
            _companiesState = companiesState;
        }

        public bool HasValues() => _tsksState.HasValue(true) &&
                               _initiativesState.HasValue(true) &&
                               _projectsState.HasValue(true) &&
                               _companiesState.HasValue(true);

        public override Task HandleAsync(LoadSuccessAction<Tsk> action, IDispatcher dispatcher) {


            return Task.CompletedTask;
        }
    }


    //public class SetApplicationSettingEffect : Effect<SetApplicationSettingAction> {

    //    protected readonly ILogger<SetApplicationSettingEffect> _logger;

    //    public SetApplicationSettingEffect(ILogger<SetApplicationSettingEffect> logger) => _logger = logger;

    //    public override Task HandleAsync(SetApplicationSettingAction action, IDispatcher dispatcher) {
    //        try {
    //            _logger.LogInformation($"Setting {action.ApplicationSetting.GetType().Name} ...");

    //            dispatcher.Dispatch(new SetApplicationSettingSuccessAction(action.ApplicationSetting));
    //        } catch (Exception e) {
    //            _logger.LogError($"Error, reason: {e}");
    //            dispatcher.Dispatch(new SetApplicationSettingFailureAction(e.Message));
    //        }
    //        return Task.CompletedTask;
    //    }
    //}

}
