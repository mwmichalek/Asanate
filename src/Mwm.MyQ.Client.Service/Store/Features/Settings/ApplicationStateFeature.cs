using Fluxor;
using Mwm.MyQ.Client.Service.Store.State.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.MyQ.Client.Service.Store.Features.Settings;

public class ApplicationStateFeature : Feature<ApplicationState> {

    public override string GetName() => typeof(ApplicationState).Name;

    protected override ApplicationState GetInitialState() => Activator.CreateInstance<ApplicationState>();

}
