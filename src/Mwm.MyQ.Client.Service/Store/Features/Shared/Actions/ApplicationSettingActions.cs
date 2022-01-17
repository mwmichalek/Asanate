using Mwm.MyQ.Client.Service.Store.Features.Settings;
using Mwm.MyQ.Client.Service.Store.Features.Shared.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.MyQ.Client.Service.Store.Features.Shared.Actions;

public class SetApplicationSettingAction {

    public SetApplicationSettingAction(IApplicationSetting applicationSetting) => ApplicationSetting = applicationSetting;  
    
    public IApplicationSetting ApplicationSetting { get; } 

}

public class SetApplicationSettingSuccessAction {

    public SetApplicationSettingSuccessAction(IApplicationSetting applicationSetting) => ApplicationSetting = applicationSetting;

    public IApplicationSetting ApplicationSetting { get; }

}

public class SetApplicationSettingFailureAction : FailureAction {

    public SetApplicationSettingFailureAction(string errorMessage) : base(errorMessage) { }

}

