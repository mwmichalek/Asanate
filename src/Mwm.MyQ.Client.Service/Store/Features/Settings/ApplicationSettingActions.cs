using Mwm.MyQ.Client.Service.Store.Features.Settings;
using Mwm.MyQ.Client.Service.Store.Features.Shared.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.MyQ.Client.Service.Store.Features.Settings;

public class SetApplicationSettingAction<TApplicationSetting> where TApplicationSetting : IApplicationSetting {

    public SetApplicationSettingAction(TApplicationSetting applicationSetting) => ApplicationSetting = applicationSetting;  
    
    public TApplicationSetting ApplicationSetting { get; } 

}

public class SetApplicationSettingSuccessAction<TApplicationSetting> where TApplicationSetting : IApplicationSetting {

    public SetApplicationSettingSuccessAction(TApplicationSetting applicationSetting) => ApplicationSetting = applicationSetting;

    public TApplicationSetting ApplicationSetting { get; }

}

public class SetApplicationSettingFailureAction<TApplicationSetting> : FailureAction where TApplicationSetting : IApplicationSetting {

    public SetApplicationSettingFailureAction(string errorMessage) : base(errorMessage) { }

}

