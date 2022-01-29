using Fluxor;
using Microsoft.Extensions.Logging;
using Mwm.MyQ.Client.Service.Store.Features.Shared.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.MyQ.Client.Service.Store.Features.Settings;

public class SetApplicationSettingEffect<TApplicationSetting> : Effect<SetApplicationSettingAction<TApplicationSetting>> where TApplicationSetting : IApplicationSetting {

    protected readonly ILogger<SetApplicationSettingEffect<TApplicationSetting>> _logger;

    public SetApplicationSettingEffect(ILogger<SetApplicationSettingEffect<TApplicationSetting>> logger) => _logger = logger;    

    public override Task HandleAsync(SetApplicationSettingAction<TApplicationSetting> action, IDispatcher dispatcher) {
        try {
            _logger.LogInformation($"Setting {action.ApplicationSetting.GetType().Name} ...");

            dispatcher.Dispatch(new SetApplicationSettingSuccessAction<TApplicationSetting>(action.ApplicationSetting));
        } catch (Exception e) {
            _logger.LogError($"Error, reason: {e}");
            dispatcher.Dispatch(new SetApplicationSettingFailureAction<TApplicationSetting>(e.Message));
        }
        return Task.CompletedTask;
    }
}
