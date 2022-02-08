using Mwm.MyQ.Client.Service.Store.Features.Settings;
using Mwm.MyQ.Client.Service.Store.Features.Shared.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.MyQ.Client.Service.Store.Features.Settings;

public class SetApplicationSettingAction : IApplicationSetting {

    public SetApplicationSettingAction(IApplicationSetting applicationSetting) => ApplicationSetting = applicationSetting;  
    
    public IApplicationSetting ApplicationSetting { get; } 

}

public class SetApplicationSettingSuccessAction : IApplicationSetting {

    public SetApplicationSettingSuccessAction(IApplicationSetting currentSetting, IEnumerable<IApplicationSetting> applicationSettings) => 
        (CurrentSetting, ApplicationSettings) = (currentSetting, applicationSettings);

    public IApplicationSetting CurrentSetting { get; }

    public IEnumerable<IApplicationSetting> ApplicationSettings { get;}

}

public class SetApplicationSettingFailureAction : FailureAction, IApplicationSetting {

    public SetApplicationSettingFailureAction(string errorMessage) : base(errorMessage) { }

}