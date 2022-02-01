using Mwm.MyQ.Client.Service.Components;
using Mwm.MyQ.Client.Service.Models;
using Mwm.MyQ.Domain;
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

public class ObjectApplicationSetting<TClass> : ApplicationSetting where TClass : class {

    public TClass PreviousValue { get; set; }

    public TClass CurrentValue { get; set; }

}

public class PrimativeApplicationSetting<TPrimative> : ApplicationSetting, IPrimativeApplicationSetting where TPrimative : struct {

    public TPrimative PreviousValue { get; set; }

    public TPrimative CurrentValue { get; set; }

}


