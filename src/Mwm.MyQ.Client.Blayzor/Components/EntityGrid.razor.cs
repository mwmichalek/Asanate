using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Mwm.MyQ.Client.Blayzor.Models.Tsks;
using Mwm.MyQ.Client.Service.Facades;
using Mwm.MyQ.Client.Service.Store.State.Shared;
using Mwm.MyQ.Domain;
using System.Threading.Tasks;

namespace Mwm.MyQ.Client.Blayzor.Components;

public partial class EntityGrid : TskModelConsumerComponent {

    [Inject]
    ILogger<EntityGrid> Logger { get; set; }

    protected override async Task OnInitializedAsync() {
        await base.OnInitializedAsync();

        //ApplicationState.StateChanged += (s, e) => UpdateSettings(e);
    }

    //private void UpdateSettings(ApplicationState applicationState) {
    //    Logger.LogInformation($"Updated setting: {applicationState.CurrentSetting.GetType().Name}");
    //}

}