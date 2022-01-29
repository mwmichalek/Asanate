﻿using Fluxor;
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
    public static ApplicationState ReduceSetApplicationSettingAction(ApplicationState state, SetApplicationSettingAction<IsInFocusOnlyFlag> _) =>
           new ApplicationState(true, null, state.Settings, null);

    [ReducerMethod]
    public static ApplicationState ReduceSetApplicationSettingSuccessAction(ApplicationState state, SetApplicationSettingSuccessAction<IsInFocusOnlyFlag> action) {
        var settings = state.Settings.ToList();
        var setting = state.FindByType(action.ApplicationSetting.GetType());
        if (setting != null)
            settings.Remove(setting);
        settings.Add(action.ApplicationSetting);

        return new ApplicationState(false, null, settings, action.ApplicationSetting);
    }

    [ReducerMethod]
    public static ApplicationState ReduceSetApplicationSettingFailureAction(ApplicationState state, SetApplicationSettingFailureAction<IsInFocusOnlyFlag> action) =>
        new ApplicationState(false, action.ErrorMessage, state.Settings, state.CurrentSetting);




    [ReducerMethod]
    public static ApplicationState ReduceSetApplicationSettingAction(ApplicationState state, SetApplicationSettingAction<IsGroupedByCompanyFlag> _) =>
           new ApplicationState(true, null, state.Settings, null);

    [ReducerMethod]
    public static ApplicationState ReduceSetApplicationSettingSuccessAction(ApplicationState state, SetApplicationSettingSuccessAction<IsGroupedByCompanyFlag> action) {
        var settings = state.Settings.ToList();
        var setting = state.FindByType(action.ApplicationSetting.GetType());
        if (setting != null)
            settings.Remove(setting);
        settings.Add(action.ApplicationSetting);

        return new ApplicationState(false, null, settings, action.ApplicationSetting);
    }

    [ReducerMethod]
    public static ApplicationState ReduceSetApplicationSettingFailureAction(ApplicationState state, SetApplicationSettingFailureAction<IsGroupedByCompanyFlag> action) =>
        new ApplicationState(false, action.ErrorMessage, state.Settings, state.CurrentSetting);





    [ReducerMethod]
    public static ApplicationState ReduceSetApplicationSettingAction(ApplicationState state, SetApplicationSettingAction<IsActionStatusOnlyFlag> _) =>
           new ApplicationState(true, null, state.Settings, null);

    [ReducerMethod]
    public static ApplicationState ReduceSetApplicationSettingSuccessAction(ApplicationState state, SetApplicationSettingSuccessAction<IsActionStatusOnlyFlag> action) {
        var settings = state.Settings.ToList();
        var setting = state.FindByType(action.ApplicationSetting.GetType());
        if (setting != null)
            settings.Remove(setting);
        settings.Add(action.ApplicationSetting);

        return new ApplicationState(false, null, settings, action.ApplicationSetting);
    }

    [ReducerMethod]
    public static ApplicationState ReduceSetApplicationSettingFailureAction(ApplicationState state, SetApplicationSettingFailureAction<IsActionStatusOnlyFlag> action) =>
        new ApplicationState(false, action.ErrorMessage, state.Settings, state.CurrentSetting);





}
