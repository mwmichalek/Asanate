using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using Mwm.MyQ.Client.Blayzor.Helpers;
using Mwm.MyQ.Client.Service.Models;
using Mwm.MyQ.Client.Service.Facades;
using Mwm.MyQ.Client.Service.Store.Features.Settings;
using Mwm.MyQ.Client.Service.Store.Features.Shared.Helpers;
using Mwm.MyQ.Client.Service.Store.State.Shared;
using Mwm.MyQ.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mwm.MyQ.Client.Blayzor.Components;


public abstract class ModelConsumerComponent<TModel, TEntity> : FluxorComponent where TModel : EntityModel<TEntity>
                                                                                where TEntity : INamedEntity {

}





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
    public IActionSubscriber ActionSubscriber { get; set; }

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

    protected override async Task OnInitializedAsync() {
        //if (!TsksState.HasValue())
        //    await EntityStateFacade.Load<Tsk>();
        //if (!InitiativesState.HasValue())
        //    await EntityStateFacade.Load<Initiative>();
        //if (!CompaniesState.HasValue())
        //    await EntityStateFacade.Load<Company>();
        //if (!ProjectsState.HasValue())
        //    await EntityStateFacade.Load<Project>();

        //ActionSubscriber.SubscribeToAction<>

        TsksState.StateChanged += async (s, e) => await HandleUpdateAsync(e.CurrentEntity);
        InitiativesState.StateChanged += async (s, e) => await HandleUpdateAsync(e.CurrentEntity);
        ProjectsState.StateChanged += async (s, e) => await HandleUpdateAsync(e.CurrentEntity);
        CompaniesState.StateChanged += async (s, e) => await HandleUpdateAsync(e.CurrentEntity);
        ApplicationState.StateChanged += async (s, e) => await RouteApplicationSettingChangeAsync(e.CurrentSetting);

        await base.OnInitializedAsync();

        // Go through all application settings and trigger them.
        foreach (var applicationSetting in ApplicationState.Value.Settings)
            await RouteApplicationSettingChangeAsync(applicationSetting);
    }

    private async Task RouteApplicationSettingChangeAsync(IApplicationSetting applicationSetting) {
        if (applicationSetting is IsInFocusOnlyTskFilter focusFilter)
            await HandleUpdateAsync(focusFilter);
        else if (applicationSetting is IsGroupedByCompanyFlag groupingFlag)
            await HandleUpdateAsync(groupingFlag);
        else if (applicationSetting is IsActionStatusOnlyFlag actionFlag)
            await HandleUpdateAsync(actionFlag);
    }

    protected virtual Task HandleUpdateAsync(Tsk tsk) => Task.CompletedTask;

    protected virtual Task HandleUpdateAsync(Initiative initiative) => Task.CompletedTask;

    protected virtual Task HandleUpdateAsync(Project project) => Task.CompletedTask;

    protected virtual Task HandleUpdateAsync(Company company) => Task.CompletedTask;

    protected virtual Task HandleUpdateAsync(IsInFocusOnlyTskFilter filter) => Task.CompletedTask;

    protected virtual Task HandleUpdateAsync(IsGroupedByCompanyFlag flag) => Task.CompletedTask;

    protected virtual Task HandleUpdateAsync(IsActionStatusOnlyFlag flag) => Task.CompletedTask;

}
