using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Mwm.MyQ.Client.Service.Facades;
using Mwm.MyQ.Client.Service.Models;
using Mwm.MyQ.Client.Service.Store.State.Shared;
using Mwm.MyQ.Domain;
using System.Threading.Tasks;

namespace Mwm.MyQ.Client.Mud.Components;

public partial class TskDialog: ComponentBase {

    [Inject]
    public ILogger<TskDialog> Logger { get; set; }

    [Inject]
    public EntityStateFacade EntityStateFacade { get; set; }

    [Inject]
    public IState<ModelState<TskModel, Tsk>> ModelsState { get; set; }

    public bool IsDialogShowing { get; set; }

    public bool IsNew => TskModel != null && TskModel.Id == 0;

    public TskModel TskModel;

    protected override async Task OnInitializedAsync() {
        ModelsState.StateChanged += async (s, e) => {
            if (e.CurrentModel != null)
                Update(e.CurrentModel);
            else
                Close();
        };
    }

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
