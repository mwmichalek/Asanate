using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Mwm.MyQ.Client.Service.Components;
using Mwm.MyQ.Client.Service.Facades;
using Mwm.MyQ.Client.Service.Models;
using Mwm.MyQ.Client.Service.Store.Features.Settings;
using Mwm.MyQ.Client.Service.Store.Features.Shared.Helpers;
using Mwm.MyQ.Client.Service.Store.State.Shared;
using Mwm.MyQ.Domain;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Mwm.MyQ.Client.Blayzor.Components;

public abstract class ModelConsumerComponent<TModel, TEntity> : FluxorComponent where TModel : EntityModel<TEntity>
                                                                                where TEntity : INamedEntity {
    [Inject]
    public ILogger<ModelConsumerComponent<TModel, TEntity>> Logger { get; set; }

    [Inject]
    public IState<ModelState<TModel, TEntity>> ModelsState { get; set; }

    [Inject]
    public IState<ApplicationState> ApplicationState { get; set; }

    [Inject]
    public IActionSubscriber ActionSubscriber { get; set; }

    [Inject]
    public EntityStateFacade EntityStateFacade { get; set; }

    public bool HasValues() => ModelsState.HasValue(false);

    public bool HasErrors() => ModelsState.HasErrors();

    public bool IsLoading() => ModelsState.IsLoading();

    protected override async Task OnInitializedAsync() {
        ModelsState.StateChanged += async (s, e) => await HandleModelsLoaded();
        ApplicationState.StateChanged += async (s, e) => await ApplyTo(e.CurrentSetting);
        //ActionSubscriber.SubscribeToAction<IApplicationSetting>(this, (setting) => { });
        await base.OnInitializedAsync();

        foreach (var applicationSetting in ApplicationState.Value.Settings) 
            await ApplyTo(applicationSetting);
    }

    public Task ApplyTo(IApplicationSetting applicationSetting) {
        var consumerType = typeof(IApplicationSettingConsumer<>).MakeGenericType(applicationSetting.GetType());
        var implementsConsumerType = consumerType.IsAssignableFrom(this.GetType());
        if (implementsConsumerType) {
            var consumerConcreteType = this.GetType();
            var consumerMethod = consumerConcreteType.GetMethod("ApplySetting", new[] { applicationSetting.GetType() });
            var result = consumerMethod.Invoke(this, new[] { applicationSetting });
        }
        return Task.CompletedTask;
    }

    protected virtual Task HandleModelsLoaded() => Task.CompletedTask;

}
