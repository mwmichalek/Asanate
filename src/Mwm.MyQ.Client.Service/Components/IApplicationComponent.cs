using Mwm.MyQ.Client.Service.Store.Features.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.MyQ.Client.Service.Components;

//public interface IApplicationComponent {


//}

public interface IApplicationSettingConsumer<TApplicationSetting> : IApplicationSettingConsumer where TApplicationSetting : IApplicationSetting {

    Task ApplySetting(TApplicationSetting applicationSetting);

}

public interface IApplicationSettingConsumer {

}
