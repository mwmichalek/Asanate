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

namespace Mwm.MyQ.Client.Mud.Components;

public abstract class EventListenerComponent : FluxorComponent {

    [Inject]
    public ILogger<EventListenerComponent> Logger { get; set; }

    [Inject]
    public IState<ApplicationState> ApplicationState { get; set; }

    [Inject]
    public IActionSubscriber ActionSubscriber { get; set; }

    protected override async Task OnInitializedAsync() {
        ApplicationState.StateChanged += async (s, e) => await ApplyTo(e.CurrentSetting);

        foreach (var applicationSetting in ApplicationState.Value.Settings) 
            await ApplyTo(applicationSetting);

        await base.OnInitializedAsync();
    }

    public Task ApplyTo(IApplicationSetting applicationSetting) {
        if (applicationSetting != null) { 
            var consumerType = typeof(IApplicationSettingConsumer<>).MakeGenericType(applicationSetting.GetType());
            var implementsConsumerType = consumerType.IsAssignableFrom(this.GetType());
            if (implementsConsumerType) {
                var consumerConcreteType = this.GetType();
                var consumerMethod = consumerConcreteType.GetMethod("ApplySetting", new[] { applicationSetting.GetType() });
                var result = consumerMethod.Invoke(this, new[] { applicationSetting });
            }
        } 
        return Task.CompletedTask;
    }

}
