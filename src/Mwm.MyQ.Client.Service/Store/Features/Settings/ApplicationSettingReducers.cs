using Fluxor;
using Mwm.MyQ.Client.Service.Store.Features.Shared.Actions;
using Mwm.MyQ.Client.Service.Store.State.Shared;
using Mwm.MyQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.MyQ.Client.Service.Store.Features.Settings;

public static class ApplicationSettingReducers {

    [ReducerMethod]
    public static ApplicationState ReduceSetApplicationSettingAction(ApplicationState state, SetApplicationSettingAction _) =>
           new ApplicationState(true, null, state.Settings, null);

    [ReducerMethod]
    public static ApplicationState ReduceSetApplicationSettingSuccessAction(ApplicationState state, SetApplicationSettingSuccessAction action) {
        return new ApplicationState(false, null, action.ApplicationSettings, action.CurrentSetting);
    }

    [ReducerMethod]
    public static ApplicationState ReduceSetApplicationSettingFailureAction(ApplicationState state, SetApplicationSettingFailureAction action) =>
        new ApplicationState(false, action.ErrorMessage, state.Settings, state.CurrentSetting);

}
