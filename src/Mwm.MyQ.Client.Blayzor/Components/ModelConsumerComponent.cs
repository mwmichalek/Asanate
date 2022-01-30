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

        //    info: Mwm.MyQ.Client.Blayzor.Components.ModelConsumerComponent[0]
        //  >>> INTERFACE: Microsoft.AspNetCore.Components.IComponent
        //info: Mwm.MyQ.Client.Blayzor.Components.ModelConsumerComponent[0]
        //  >>> INTERFACE: System.IDisposable
        //info: Mwm.MyQ.Client.Blayzor.Components.ModelConsumerComponent[0]
        //  >>> INTERFACE: Microsoft.AspNetCore.Components.IHandleAfterRender
        //info: Mwm.MyQ.Client.Blayzor.Components.ModelConsumerComponent[0]
        //  >>> INTERFACE: Microsoft.AspNetCore.Components.IHandleEvent
        //info: Mwm.MyQ.Client.Blayzor.Components.ModelConsumerComponent[0]
        //  >>> INTERFACE: Mwm.MyQ.Client.Service.Components.IApplicationSettingConsumer`1[Mwm.MyQ.Client.Service.Store.Features.Settings.IsActionStatusOnlyFlag]
        //info: Mwm.MyQ.Client.Blayzor.Components.ModelConsumerComponent[0]
        //      >>> INTERFACE: Mwm.MyQ.Client.Service.Components.IApplicationSettingConsumer`1[Mwm.MyQ.Client.Service.Store.Features.Settings.IsGroupedByCompanyFlag]
        //info: Mwm.MyQ.Client.Blayzor.Components.ModelConsumerComponent[0]
        //      >>> INTERFACE: Mwm.MyQ.Client.Service.Components.IApplicationSettingConsumer`1[Mwm.MyQ.Client.Service.Store.Features.Settings.IsInFocusOnlyFlag]
        //info: Mwm.MyQ.Client.Blayzor.Components.ModelConsumerComponent[0]
        //      >>> INTERFACE: Mwm.MyQ.Client.Service.Components.IApplicationSettingConsumer


        //foreach (var i in this.GetType().GetInterfaces()) {
            //var consumerType = typeof(IApplicationSettingConsumer<>).MakeGenericType(typeof(IsActionStatusOnlyFlag));
            //


            
        //}

        // Go through all application settings and trigger them.
        foreach (var applicationSetting in ApplicationState.Value.Settings) {
            var consumerType = typeof(IApplicationSettingConsumer<>).MakeGenericType(applicationSetting.GetType());
            var implementsConsumerType = consumerType.IsAssignableFrom(this.GetType());
            if (implementsConsumerType) { 
            }
            Logger.LogInformation($">>> INTERFACE: {consumerType} : {implementsConsumerType}");

            await ApplyTo(applicationSetting);
        }
    }

    public async Task ApplyTo<TSetting>(TSetting setting) where TSetting : IApplicationSetting {
        Logger.LogInformation($"ApplyTo: {typeof(TSetting).Name}");
        if (this is IApplicationSettingConsumer<TSetting> applicationSettingConsumer)
            await applicationSettingConsumer.ApplySetting(setting);
    }

    protected virtual Task HandleModelsLoaded() => Task.CompletedTask;

}
