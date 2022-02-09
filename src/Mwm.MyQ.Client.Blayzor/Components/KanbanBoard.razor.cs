﻿using Microsoft.Extensions.Logging;
using Mwm.MyQ.Application.Tsks.Commands;
using Mwm.MyQ.Client.Blayzor.Helpers;
using Mwm.MyQ.Client.Service.Components;
using Mwm.MyQ.Client.Service.Facades;
using Mwm.MyQ.Client.Service.Models;
using Mwm.MyQ.Client.Service.Store.Features.Settings;
using Mwm.MyQ.Client.Service.Store.Features.Shared.Helpers;
using Mwm.MyQ.Domain;
using Syncfusion.Blazor.Kanban;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mwm.MyQ.Client.Blayzor.Components;

public partial class KanbanBoard : ModelConsumerComponent<TskModel, Tsk>, 
                                   IApplicationSettingConsumer<IsInFocusOnlyFlag>,
                                   IApplicationSettingConsumer<IsGroupedByCompanyFlag>,
                                   IApplicationSettingConsumer<IsActionStatusOnlyFlag> {

    private SfKanban<TskModel> refKanbanBoard;

    private SfKanban<TskModel> RefKanbanBoard {
        get { return refKanbanBoard; }
        set { 
            refKanbanBoard = value;
        }
    }

    private TskPopup refTskPopup;

    private List<TskModel> filteredTskModels = new List<TskModel>();

    public IEnumerable<TskModel> FilteredTskModels { 
        get => filteredTskModels;
        set { } 
    }

    private List<Status> statuses = StatusExtensions.AllStatuses;

    public List<Status> Statuses {
        get => statuses;
        set { }
    }

    protected override async Task OnInitializedAsync() {
        Logger.LogInformation($">>> OnInitializedAsync triggered.");
        await base.OnInitializedAsync();
        await InitializeBoardAsync();
    }

    protected override async Task HandleModelsLoaded() {
        Logger.LogInformation($">>> HandleModelsLoaded triggered.");
        await InitializeBoardAsync();
        
    }

    protected override Task OnAfterRenderAsync(bool firstRender) {
        Logger.LogInformation($"OnAfterRenderAsync : {firstRender}");
        return base.OnAfterRenderAsync(firstRender);
    }

    private async Task InitializeBoardAsync() {
        if (HasValues()) {
            Logger.LogDebug($">>> Initialization Started, models[{filteredTskModels.Count}]");
            filteredTskModels = ModelsState.Value.FilteredModels.ToList();
            UpdateSwimLanes();
            UpdateColumns();
            await RefreshBoardAsync();
            Logger.LogDebug($">>> Initialization Completed, models[{filteredTskModels.Count}]");
        } else
            Logger.LogDebug($"Not ready to be initialized.");
    }

    private void UpdateSwimLanes() {
        if (refKanbanBoard != null) {
            if (IsGroupedByCompany) {
                refKanbanBoard.SwimlaneSettings = new KanbanSwimlaneSettings {
                    SortDirection = SortDirection.Ascending,
                    KeyField = "CompanyName",
                    TextField = "CompanyName"
                };
            } else {
                refKanbanBoard.SwimlaneSettings = new KanbanSwimlaneSettings {
                    SortDirection = SortDirection.Ascending,
                    KeyField = string.Empty,
                    TextField = string.Empty
                };
            }
        }
    }

    private void UpdateColumns() {
        if (refKanbanBoard != null) {
            refKanbanBoard.Columns = statuses.Select(s => s.ToStr())
                                             .Select(s => new KanbanColumn {
                                                 HeaderText = s,
                                                 KeyField = s.ToKeyFields()
                                             }).ToList();
        }
    }

    private async Task RefreshBoardAsync() {
        if (refKanbanBoard != null)
            await refKanbanBoard.RefreshAsync();
    }

    //####################################### ACTIONS ################################

    public async Task DragStopHandlerAsync(DragEventArgs<TskModel> args) {
        foreach (var updatedTskModel in args.Data) {
            try {


                var originalTskModel = ModelsState.FindById(updatedTskModel.Id);

                if (updatedTskModel.Status != originalTskModel.Status) {
                    Logger.LogInformation($"Moved: {updatedTskModel.Name}, FromStatus: {originalTskModel.Status} ToStatus: {updatedTskModel.Status}");
                    await EntityStateFacade.Update<Tsk, TskUpdate.Command>(new TskUpdate.Command {
                        Id = updatedTskModel.Id,
                        Name = originalTskModel.Name,
                        Status = updatedTskModel.Status
                    });
                }
            } catch (Exception ex) {
                Logger.LogError($"Unable to update: {updatedTskModel.Name}, {ex}");
            }
        }
    }

    public void ShowTskEditor(DialogOpenEventArgs<TskModel> args) {
        args.Cancel = true;
        refTskPopup.Update(args.Data);
    }

    //protected override async Task HandleUpdateAsync(IsGroupedByCompanyFlag flag) => await SetIsGroupedByCompany(flag.CurrentValue);

    public bool IsGroupedByCompany { get; set; } = true;

    public async Task SetIsGroupedByCompany(bool isGroupedByCompany) {
        IsGroupedByCompany = isGroupedByCompany;
        UpdateSwimLanes();
        await RefreshBoardAsync();
    }

    //protected override Task HandleUpdateAsync(IsInFocusOnlyTskFilter filter) => SetIsInFocusOnly(filter.CurrentValue);

    public bool IsInFocusOnly { get; set; } = false;

    public Task SetIsInFocusOnly(bool isInFocusOnly) {
        IsInFocusOnly = isInFocusOnly;
        //FilterTskModels();
        //StateHasChanged(); //Because a collection changed.
        return Task.CompletedTask;
    }

    //protected override async Task HandleUpdateAsync(IsActionStatusOnlyFlag flag) => await SetIsActionStatusOnly(flag.CurrentValue);

    public async Task SetIsActionStatusOnly(bool isActionStatusOnly) {
        statuses = isActionStatusOnly ? StatusExtensions.ActionStatuses : StatusExtensions.AllStatuses;
        UpdateColumns();
        await RefreshBoardAsync();
    }

    public async Task ApplySetting(IsInFocusOnlyFlag applicationSetting) {
        await SetIsInFocusOnly(applicationSetting.CurrentValue);
    }

    public async Task ApplySetting(IsGroupedByCompanyFlag applicationSetting) {
        await SetIsGroupedByCompany(applicationSetting.CurrentValue);
    }

    public async Task ApplySetting(IsActionStatusOnlyFlag applicationSetting) {
        await SetIsActionStatusOnly(applicationSetting.CurrentValue);
    }
}
