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
using System.Collections.Generic;
using System.Linq;
using Syncfusion.Blazor.Grids;

namespace Mwm.MyQ.Client.Blayzor.Components;

public partial class TskModelGrid : ModelConsumerComponent<TskModel, Tsk> {

    private SfGrid<TskModel> refSfGrid;

    private TskPopup refTskPopup;

    private List<TskModel> filteredTskModels = new List<TskModel>();

    public IEnumerable<TskModel> FilteredTskModels {
        get => filteredTskModels;
        set { }
    }

    protected override async Task OnInitializedAsync() {
        await base.OnInitializedAsync();
        await InitializeGridAsync();
    }

    protected override async Task HandleModelsLoaded() {
        //TODO: This is getting called way too many times
        await InitializeGridAsync();
    }

    private Task InitializeGridAsync() {
        if (HasValues()) {
            Logger.LogDebug($">>> InitializeGridAsync Started, models[{filteredTskModels.Count}]");

            filteredTskModels = ModelsState.Value.FilteredModels.ToList();
            //refSfGrid?.Height = "100%";
            //StateHasChanged();
            Logger.LogDebug($">>> InitializeGridAsync Completed, models[{filteredTskModels.Count}]");
        } else
            Logger.LogDebug($"Not ready to be initialized.");
        return Task.CompletedTask;
    }

    public void ShowTskEditor(TskModel tskModel) {
        refTskPopup.Update(tskModel);
    }

}