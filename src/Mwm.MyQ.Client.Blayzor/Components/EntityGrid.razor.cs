using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Mwm.MyQ.Client.Service.Facades;
using Mwm.MyQ.Client.Service.Store.State.Shared;
using Mwm.MyQ.Domain;

namespace Mwm.MyQ.Client.Blayzor.Components;

public partial class EntityGrid : EntityFluxorComponent {

    [Inject]
    ILogger<EntityGrid> Logger { get; set; }

    protected override void OnInitialized() {
        base.OnInitialized(); 

    }

}