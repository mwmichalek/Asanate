using Fluxor;
using Microsoft.Extensions.Logging;
using Mwm.MyQ.Client.Service.Store.Features.Shared.Actions;
using Mwm.MyQ.Client.Service.Store.State.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.MyQ.Client.Service.Store.Features.Settings;

public class SetApplicationSettingEffect : Effect<SetApplicationSettingAction>  {

    protected readonly ILogger<SetApplicationSettingEffect> _logger;

    protected IState<ApplicationState> _applicationState { get; set; }

    public SetApplicationSettingEffect(ILogger<SetApplicationSettingEffect> logger, IState<ApplicationState> applicationState) => 
        (_logger, _applicationState) = (logger, applicationState);    

    public override Task HandleAsync(SetApplicationSettingAction action, IDispatcher dispatcher) {
        try {
            _logger.LogInformation($"Setting {action.ApplicationSetting.GetType().Name} ...");

            var settings = _applicationState.Value.Settings.ToList();
            var setting = _applicationState.Value.FindByType(action.ApplicationSetting.GetType());
            if (setting != null)
                settings.Remove(setting);
            settings.Add(action.ApplicationSetting);

            dispatcher.Dispatch(new SetApplicationSettingSuccessAction(action.ApplicationSetting, settings));
        } catch (Exception e) {
            _logger.LogError($"Error, reason: {e}");
            dispatcher.Dispatch(new SetApplicationSettingFailureAction(e.Message));
        }
        return Task.CompletedTask;
    }
}
