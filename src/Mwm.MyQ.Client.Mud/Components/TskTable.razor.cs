using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Mwm.MyQ.Client.Mud.Components;
using Mwm.MyQ.Client.Service.Facades;
using Mwm.MyQ.Client.Service.Models;
using Mwm.MyQ.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mwm.MyQ.Application.Tsks.Commands;

namespace Mwm.MyQ.Client.Mud.Components;

public partial class TskTable : ModelConsumerComponent<TskModel, Tsk> {

    [Inject]
    public ModelFacade ModelFacade { get; set; }

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

            Logger.LogDebug($">>> InitializeGridAsync Completed, models[{filteredTskModels.Count}]");
        } else
            Logger.LogDebug($"Not ready to be initialized.");
        return Task.CompletedTask;
    }

    public async Task Edit(TskModel tskModel) {
        await Task.Run(() => ModelFacade.Edit<TskModel, Tsk>(tskModel));
    }

    public async Task Archive(TskModel tskModel) {
        await EntityStateFacade.Update<Tsk, TskUpdate.Command>(new TskUpdate.Command {
            Id = tskModel.Id,
            Name = tskModel.Name,
            IsArchived = true
        });
    }

    public async Task Complete(TskModel tskModel) {
        await EntityStateFacade.Update<Tsk, TskUpdate.Command>(new TskUpdate.Command {
            Id = tskModel.Id,
            Name = tskModel.Name,
            IsCompleted = true
        });
    }

}