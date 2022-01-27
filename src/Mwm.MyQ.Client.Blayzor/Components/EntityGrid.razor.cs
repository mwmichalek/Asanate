using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Mwm.MyQ.Client.Service.Models;
using Mwm.MyQ.Client.Service.Facades;
using Mwm.MyQ.Client.Service.Store.State.Shared;
using Mwm.MyQ.Domain;
using Syncfusion.Blazor.Kanban;
using System.Threading.Tasks;
using Mwm.MyQ.Client.Service.Components;

namespace Mwm.MyQ.Client.Blayzor.Components;

public partial class EntityGrid : TskModelConsumerComponent, IApplicationComponent {

    [Inject]
    ILogger<EntityGrid> Logger { get; set; }

    private TskPopup refTskPopup;


    protected override async Task OnInitializedAsync() {
        await base.OnInitializedAsync();

        //ApplicationState.StateChanged += (s, e) => UpdateSettings(e);
    }

    public void ShowTskEditor(TskModel tskModel) {
        refTskPopup.Update(tskModel);
    }

    //private void UpdateSettings(ApplicationState applicationState) {
    //    Logger.LogInformation($"Updated setting: {applicationState.CurrentSetting.GetType().Name}");
    //}

}