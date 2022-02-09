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

namespace Mwm.MyQ.Client.Blayzor.Components;

public partial class EntityGrid : ModelConsumerComponent<TskModel, Tsk> {

    private TskPopup refTskPopup;

    private List<TskModel> filteredTskModels = new List<TskModel>();

    public IEnumerable<TskModel> FilteredTskModels {
        get => filteredTskModels;
        set { }
    }

    protected override async Task OnInitializedAsync() {
        await base.OnInitializedAsync();
    }

    public void ShowTskEditor(TskModel tskModel) {
        refTskPopup.Update(tskModel);
    }

}