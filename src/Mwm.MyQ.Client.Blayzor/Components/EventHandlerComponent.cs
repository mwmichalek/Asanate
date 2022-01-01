using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using Mwm.MyQ.Client.Blayzor.Helpers;
using Mwm.MyQ.Client.Blayzor.Models.Tsks;
using Mwm.MyQ.Client.Service.Facades;
using Mwm.MyQ.Client.Service.Store.Features.Settings;
using Mwm.MyQ.Client.Service.Store.Features.Shared.Helpers;
using Mwm.MyQ.Client.Service.Store.State.Shared;
using Mwm.MyQ.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Mwm.MyQ.Client.Blayzor.Components;

public abstract class EventHandlerComponent : FluxorComponent {

    [Inject]
    public IState<EntityState<Tsk>> TsksState { get; set; }

    [Inject]
    public IState<EntityState<Initiative>> InitiativesState { get; set; }

    [Inject]
    public IState<EntityState<Project>> ProjectsState { get; set; }

    [Inject]
    public IState<EntityState<Company>> CompaniesState { get; set; }

    [Inject]
    public IState<ApplicationState> ApplicationState { get; set; }

    [Inject]
    public EntityStateFacade EntityStateFacade { get; set; }

    public bool IsLoading() => TsksState.IsLoading() ||
                           InitiativesState.IsLoading() ||
                           ProjectsState.IsLoading() ||
                           CompaniesState.IsLoading();

    public bool HasErrors() => TsksState.HasErrors() ||
                               InitiativesState.HasErrors() ||
                               ProjectsState.HasErrors() ||
                               CompaniesState.HasErrors();

    public bool HasValues() => TsksState.HasValue(true) &&
                               InitiativesState.HasValue(true) &&
                               ProjectsState.HasValue(true) &&
                               CompaniesState.HasValue(true);

    protected override void OnInitialized() {
        if (!TsksState.HasValue())
            EntityStateFacade.Load<Tsk>();
        if (!InitiativesState.HasValue())
            EntityStateFacade.Load<Initiative>();
        if (!CompaniesState.HasValue())
            EntityStateFacade.Load<Company>();
        if (!ProjectsState.HasValue())
            EntityStateFacade.Load<Project>();


        TsksState.StateChanged += (s, e) => Handle(e.CurrentEntity);
        InitiativesState.StateChanged += (s, e) => Handle(e.CurrentEntity);
        ProjectsState.StateChanged += (s, e) => Handle(e.CurrentEntity);
        CompaniesState.StateChanged += (s, e) => Handle(e.CurrentEntity);

        ApplicationState.StateChanged += (s, e) => RouteApplicationSettingChange(e.CurrentSetting);

        base.OnInitialized();
    }

    private void RouteApplicationSettingChange(IApplicationSetting applicationSetting) {
        if (applicationSetting is IsInFocusOnlyTskFilter focusFilter)
            Handle(focusFilter);
        else if (applicationSetting is IsGroupedByCompanyFlag groupingFlag)
            Handle(groupingFlag);
        else if (applicationSetting is IsActionStatusOnlyFlag actionFlag)
            Handle(actionFlag);
    }

    protected virtual void Handle(Tsk tsk) { }

    protected virtual void Handle(Initiative initiative) { }

    protected virtual void Handle(Project project) { }

    protected virtual void Handle(Company company) { }

    protected virtual void Handle(IsInFocusOnlyTskFilter filter) { }

    protected virtual void Handle(IsGroupedByCompanyFlag flag) { }

    protected virtual void Handle(IsActionStatusOnlyFlag flag) { }

}
