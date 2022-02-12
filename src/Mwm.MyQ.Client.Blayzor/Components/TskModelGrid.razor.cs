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

namespace Mwm.MyQ.Client.Blayzor.Components;

public partial class TskModelGrid : ModelConsumerComponent<TskModel, Tsk> {

    private TskPopup refTskPopup;

    private List<TskModel> filteredTskModels = new List<TskModel>();

    public IEnumerable<TskModel> FilteredTskModels {
        get => filteredTskModels;
        set { }
    }

    protected override async Task OnInitializedAsync() {
        await base.OnInitializedAsync();
    }

    protected override async Task HandleModelsLoaded() {
        Logger.LogInformation($">>> HandleModelsLoaded triggered.");
        filteredTskModels = ModelsState.Value.FilteredModels.ToList();
    }

    public void ShowTskEditor(TskModel tskModel) {
        refTskPopup.Update(tskModel);
    }

}