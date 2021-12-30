using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.MyQ.Client.Service.Store.Features.Settings;

public interface IApplicationSetting { }

public interface IPrimativeApplicationSetting : IApplicationSetting { }

public interface IObjectApplicationSetting : IApplicationSetting { }

public abstract class ApplicationSetting : IApplicationSetting {
}

public abstract class ObjectApplicationSetting<TClass> : ApplicationSetting, IObjectApplicationSetting where TClass : class {

    public TClass PreviousValue { get; set; }

    public TClass CurrentValue { get; set; }

}

public abstract class PrimativeApplicationSetting<TPrimative> : ApplicationSetting, IPrimativeApplicationSetting where TPrimative : struct {

    public TPrimative PreviousValue { get; set; }

    public TPrimative CurrentValue { get; set; }

}
