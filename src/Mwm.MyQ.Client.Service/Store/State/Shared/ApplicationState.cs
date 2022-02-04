using Mwm.MyQ.Client.Service.Store.Features.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.MyQ.Client.Service.Store.State.Shared;

public class ApplicationState : RootState {

    public IEnumerable<IApplicationSetting>? Settings { get; private set; } 

    public IApplicationSetting FindByType(Type type) => (_lookup != null) ? _lookup[type].SingleOrDefault() : default;

    public IApplicationSetting? CurrentSetting { get; }

    private ILookup<Type, IApplicationSetting> _lookup;

    public ApplicationState() : this(false, null, ApplicationSettingTypes.DefaultSettings, default) {
    }

    public ApplicationState(bool isLoading = false, 
                            string? currentErrorMessage = null, 
                            IEnumerable<IApplicationSetting>? settings = null, 
                            IApplicationSetting? currentSetting = default) :
        base(isLoading, currentErrorMessage) {
        _lookup = settings?.ToLookup(e => e.GetType());
        Settings = settings;
        CurrentSetting = currentSetting;
    }
}


