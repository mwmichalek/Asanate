using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using MudBlazor;
using Mwm.MyQ.Application.Tsks.Commands;
using Mwm.MyQ.Client.Service.Facades;
using Mwm.MyQ.Client.Service.Models;
using Mwm.MyQ.Client.Service.Store.State.Shared;
using Mwm.MyQ.Domain;
using System;
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

    public DialogOptions DialogOptions => new DialogOptions { FullWidth = true };

    public string HeaderMessage => (IsNew) ? "ADD TSK" : "EDIT TASK";

    public TskModel TskModel { get; set; }

    protected override Task OnInitializedAsync() {
        ModelsState.StateChanged += async (s, e) => {
            if (e.CurrentModel != null)
                await Update(e.CurrentModel);
            else
                await Close();
        };
        return Task.CompletedTask;
    }

    public void Add(int? initiativeId) {
        Logger.LogInformation($"Adding TskModel.");
        TskModel = new TskModel {
            InitiativeId = initiativeId
        };
        IsDialogShowing = true;
        StateHasChanged();
    }

    public Task Update(TskModel tskModel) {
        Logger.LogInformation($"Updating TskModel.");
        TskModel = tskModel;
        IsDialogShowing = true;
        StateHasChanged();
        return Task.CompletedTask;
    }


    public async Task SaveAsync() {
        Logger.LogInformation($"Saving Dialog.");
        try {
            if (TskModel.Id == 0) {
                Logger.LogInformation($"Add: {TskModel.Name}");
                await EntityStateFacade.Add<Tsk, TskAdd.Command>(new TskAdd.Command {
                    Name = TskModel.Name,
                    Status = TskModel.Status,
                    DurationEstimate = TskModel.DurationEstimate,
                    DurationCompleted = TskModel.DurationCompleted,
                    Notes = TskModel.Notes,
                    DueDate = TskModel.DueDate,
                    StartDate = TskModel.StartDate,
                    StartedDate = TskModel.StartedDate,
                    CompletedDate = TskModel.CompletedDate
                });

            } else {
                Logger.LogInformation($"Update: {TskModel.Name}");
                await EntityStateFacade.Update<Tsk, TskUpdate.Command>(new TskUpdate.Command {
                    Id = TskModel.Id,
                    Name = TskModel.Name,
                    Status = TskModel.Status,
                    DurationEstimate = TskModel.DurationEstimate,
                    DurationCompleted = TskModel.DurationCompleted,
                    Notes = TskModel.Notes,
                    DueDate = TskModel.DueDate,
                    StartDate = TskModel.StartDate,
                    StartedDate = TskModel.StartedDate,
                    CompletedDate = TskModel.CompletedDate
                });
                
            }
            await Close();
        } catch (Exception ex) {
            Logger.LogError($"Unable to update: {TskModel.Name}, {ex}");
        }
    }

    public Task Close() {
        Logger.LogInformation($"Closing TskModel.");
        IsDialogShowing = false;
        TskModel = null;
        StateHasChanged();
        return Task.CompletedTask;
    }

}
