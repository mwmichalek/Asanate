using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Mwm.MyQ.Client.Service.Facades;
using Mwm.MyQ.Client.Service.Models;

namespace Mwm.MyQ.Client.Mud.Components;

public partial class TskDialog: ComponentBase {

    [Inject]
    public ILogger<TskDialog> Logger { get; set; }

    [Inject]
    public EntityStateFacade EntityStateFacade { get; set; }

    public bool IsDialogShowing { get; set; }

    public bool IsNew => TskModel != null && TskModel.Id == 0;

    public TskModel TskModel;

    public void Add(int? initiativeId) {
        Logger.LogInformation($"Adding TskModel.");
        TskModel = new TskModel {
            InitiativeId = initiativeId
        };
        IsDialogShowing = true;
        StateHasChanged();
    }

    public void Update(TskModel tskModel) {
        Logger.LogInformation($"Updating TskModel.");
        TskModel = tskModel;
        IsDialogShowing = true;
        StateHasChanged();
    }

    public void Close() {
        Logger.LogInformation($"Closing TskModel.");
        TskModel = null;

        IsDialogShowing = false;
        StateHasChanged();
    }

}
