using Microsoft.Extensions.Logging;
using MudBlazor;
using Mwm.MyQ.Application.Tsks.Commands;
using Mwm.MyQ.Client.Mud.Helpers;
using Mwm.MyQ.Client.Service.Components;
using Mwm.MyQ.Client.Service.Facades;
using Mwm.MyQ.Client.Service.Models;
using Mwm.MyQ.Client.Service.Store.Features.Settings;
using Mwm.MyQ.Client.Service.Store.Features.Shared.Helpers;
using Mwm.MyQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mwm.MyQ.Client.Mud.Components;

public partial class TskKanbanBoard : ModelConsumerComponent<TskModel, Tsk>, 
                                      IApplicationSettingConsumer<IsInFocusOnlyFlag>,
                                      IApplicationSettingConsumer<IsGroupedByCompanyFlag>,
                                      IApplicationSettingConsumer<IsActionStatusOnlyFlag> {

    private List<TskModel> filteredTskModels = new List<TskModel>();

    public IEnumerable<TskModel> FilteredTskModels { 
        get => filteredTskModels;
        set { } 
    }

    private List<Status> statuses;
    public List<Status> Statuses => statuses;
    

    //TODO:(MWM) This might need to change to 
    public List<string> CompanyNames => filteredTskModels.Select(x => x.CompanyName).Distinct().ToList();

    protected override async Task OnInitializedAsync() {
        Logger.LogInformation($">>> OnInitializedAsync triggered.");
        await base.OnInitializedAsync();
        await InitializeBoardAsync();
    }

    protected override async Task HandleModelsLoaded() {
        Logger.LogInformation($">>> HandleModelsLoaded triggered.");
        await InitializeBoardAsync();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender) {
        Logger.LogInformation($"OnAfterRenderAsync : {firstRender}");
        await base.OnAfterRenderAsync(firstRender);
        await InitializeBoardAsync();
    }

    private Task InitializeBoardAsync() {
        if (HasValues()) {
            Logger.LogDebug($">>> InitializeBoardAsync Started, models[{filteredTskModels.Count}]");

            filteredTskModels = ModelsState.Value.FilteredModels.OrderByDescending(fm => fm.IsInFocus).ThenBy(fm => fm.Name).ToList();

            Logger.LogDebug($">>> InitializeBoardAsync Completed, models[{filteredTskModels.Count}]");
        } else
            Logger.LogDebug($"Not ready to be initialized.");
        return Task.CompletedTask;
    }

    //private void UpdateSwimLanes() {
    //if (refKanbanBoard != null) {
    //    if (IsGroupedByCompany) {
    //        refKanbanBoard.SwimlaneSettings = new KanbanSwimlaneSettings {
    //            SortDirection = SortDirection.Ascending,
    //            KeyField = "CompanyName",
    //            TextField = "CompanyName"
    //        };
    //    } else {
    //        refKanbanBoard.SwimlaneSettings = new KanbanSwimlaneSettings {
    //            SortDirection = SortDirection.Ascending,
    //            KeyField = string.Empty,
    //            TextField = string.Empty
    //        };
    //    }
    //}
    //}

    //private void UpdateColumns() {
    //if (refKanbanBoard != null) {
    //    refKanbanBoard.Columns = statuses.Select(s => s.ToStr())
    //                                     .Select(s => new KanbanColumn {
    //                                         HeaderText = s,
    //                                         KeyField = s.ToKeyFields()
    //                                     }).ToList();
    //}
    //}

    //private async Task RefreshBoardAsync() {
    //    if (refKanbanBoard != null)
    //        await refKanbanBoard.RefreshAsync();
    //}

    //####################################### ACTIONS ################################

    private async Task DragStopHandlerAsync(MudItemDropInfo<TskModel> dropInfo) {
        var newStatus = IsGroupedByCompany ? dropInfo.DropzoneIdentifier.Split('_')[1].ToStatus() : 
                                             dropInfo.DropzoneIdentifier.ToStatus();
        var tskModel = dropInfo.Item;

        try {

            Logger.LogInformation($"Moved: {tskModel.Name}, FromStatus: {tskModel.Status} ToStatus: {newStatus}");
            await EntityStateFacade.Update<Tsk, TskUpdate.Command>(new TskUpdate.Command {
                Id = tskModel.Id,
                Name = tskModel.Name,
                Status = newStatus
            });
            
        } catch (Exception ex) {
            Logger.LogError($"Unable to update: {tskModel.Name}, {ex}");
        }
    }

    private bool CanChangeToStatus(TskModel tskModel, string statusStr) {
        if (IsGroupedByCompany) {
            var companyNameAndStatusStr = statusStr.Split('_');
            var companyName = companyNameAndStatusStr[0];
            var status = companyNameAndStatusStr[1].ToStatus();
            return tskModel.Status != status && tskModel.CompanyName == companyName;
        } else
            return tskModel.Status != statusStr.ToStatus();
    }

    //public async Task DragStopHandlerAsync(DragEventArgs<TskModel> args) {
    //    foreach (var updatedTskModel in args.Data) {
    //        try {
    //            var originalTskModel = ModelsState.FindById(updatedTskModel.Id);

    //            if (updatedTskModel.Status != originalTskModel.Status) {
    //                Logger.LogInformation($"Moved: {updatedTskModel.Name}, FromStatus: {originalTskModel.Status} ToStatus: {updatedTskModel.Status}");
    //                await EntityStateFacade.Update<Tsk, TskUpdate.Command>(new TskUpdate.Command {
    //                    Id = updatedTskModel.Id,
    //                    Name = originalTskModel.Name,
    //                    Status = updatedTskModel.Status
    //                });
    //            }
    //        } catch (Exception ex) {
    //            Logger.LogError($"Unable to update: {updatedTskModel.Name}, {ex}");
    //        }
    //    }
    //}

    //public void ShowTskEditor(DialogOpenEventArgs<TskModel> args) {
    //    args.Cancel = true;
    //    refTskPopup.Update(args.Data);
    //}

    public bool IsInFocusOnly { get; set; } = false;

    public async Task ApplySetting(IsInFocusOnlyFlag applicationSetting) {
        await SetIsInFocusOnly(applicationSetting.CurrentValue);
    }

    public Task SetIsInFocusOnly(bool isInFocusOnly) {
        IsInFocusOnly = isInFocusOnly;
        //FilterTskModels();
        StateHasChanged(); //Because a collection changed.
        return Task.CompletedTask;
    }

    public bool IsGroupedByCompany { get; set; } = true;

    public async Task ApplySetting(IsGroupedByCompanyFlag applicationSetting) {
        await SetIsGroupedByCompany(applicationSetting.CurrentValue);
    }

    public async Task SetIsGroupedByCompany(bool isGroupedByCompany) {
        IsGroupedByCompany = isGroupedByCompany;
        //UpdateSwimLanes();
        //await RefreshBoardAsync();
    }

    public async Task ApplySetting(IsActionStatusOnlyFlag applicationSetting) {
        await SetIsActionStatusOnly(applicationSetting.CurrentValue);
    }

    public async Task SetIsActionStatusOnly(bool isActionStatusOnly) {
        statuses = isActionStatusOnly ? StatusExtensions.ActionStatuses : StatusExtensions.AllStatuses;
        //UpdateColumns();
        //await RefreshBoardAsync();
    }
}
