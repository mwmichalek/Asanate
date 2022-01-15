using Mwm.MyQ.Client.Service.Facades;
using Mwm.MyQ.Client.Service.Store.Features.Shared.Actions;
using Mwm.MyQ.Client.Service.Store.Features.Shared.Helpers;
using Mwm.MyQ.Client.Service.Store.State.Shared;
using Mwm.MyQ.Application.Tsks.Commands;
using Mwm.MyQ.Data;
using Mwm.MyQ.Domain;
using Syncfusion.Blazor.Kanban;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Syncfusion.Blazor.Popups;
using Mwm.MyQ.Client.Blayzor.Components;
using Mwm.MyQ.Client.Blayzor.Helpers;
using Fluxor.Blazor.Web.Components;
using Mwm.MyQ.Client.Service.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Fluxor;
using System.Collections;
using Mwm.MyQ.Client.Service.Store.Features.Settings;

namespace Mwm.MyQ.Client.Blayzor.Components;

public partial class KanbanBoard : TskModelConsumerComponent {

    [Inject]
    ILogger<KanbanBoard> Logger { get; set; }

    private SfKanban<TskModel> refKanbanBoard;

    private SfKanban<TskModel> RefKanbanBoard {
        get { return refKanbanBoard; }
        set { 
            refKanbanBoard = value;
            InitializeBoard();
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

    //protected override async Task OnInitializedAsync() {
    //    await base.OnInitializedAsync();
    //}

    //protected override Task OnAfterRenderAsync(bool firstRender) {
    //    Logger.LogInformation($"OnAfterRenderAsync : {firstRender}");
    //    return base.OnAfterRenderAsync(firstRender);
    //}

    protected override async Task BuildTskModels() {
        await base.BuildTskModels();

        if (HasValues()) {
            var index = 0;
            foreach (var tskModel in TskModels.OrderByDescending(tm => tm.IsInFocus)
                                              .ThenBy(tm => tm.CompanyName)
                                              .ThenBy(tm => tm.ProjectAbbreviation)
                                              .ThenBy(tm => tm.InitiativeName)
                                              .ToList())
                tskModel.RankId = index++;

            Logger.LogInformation($"Built {tskModels.Count} TskModels");

            FilterTskModels();
        }
    }

    private void FilterTskModels() { 
        var filtered = tskModels as IEnumerable<TskModel>;
        if (IsInFocusOnly)
            filtered = filtered.Where(tm => tm.IsInFocus);
        filteredTskModels = filtered.ToList();
    }

    private void InitializeBoard() {
        UpdateSwimLanes();
        UpdateColumns();
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

    private async Task RefreshBoard() {
        if (refKanbanBoard != null)
            await refKanbanBoard.RefreshAsync();
    }

    //####################################### ACTIONS ################################

    public void DragStopHandler(DragEventArgs<TskModel> args) {
        foreach (var tskModel in args.Data) {
            try {
                var tsk = TsksState.FindById(tskModel.Id);
                if (tsk.Status != tskModel.Status) {
                    Logger.LogInformation($"Moved: {tskModel.Name}, FromStatus: {tsk.Status} ToStatus: {tskModel.Status}");
                    EntityStateFacade.Update<Tsk, TskUpdate.Command>(new TskUpdate.Command {
                        Id = tskModel.Id,
                        Name = tsk.Name,
                        Status = tskModel.Status
                    });
                }
            } catch (Exception ex) {
                Logger.LogError($"Unable to update: {tskModel.Name}, {ex}");
            }
        }
    }

    public void ShowTskEditor(DialogOpenEventArgs<TskModel> args) {
        args.Cancel = true;
        refTskPopup.Update(args.Data);
    }

    protected override async Task HandleUpdateAsync(IsGroupedByCompanyFlag flag) => await SetIsGroupedByCompany(flag.CurrentValue);

    public bool IsGroupedByCompany { get; set; } = true;

    public async Task SetIsGroupedByCompany(bool isGroupedByCompany) {
        IsGroupedByCompany = isGroupedByCompany;
        UpdateSwimLanes();
        await RefreshBoard();
    }

    protected override Task HandleUpdateAsync(IsInFocusOnlyTskFilter filter) => SetIsInFocusOnly(filter.CurrentValue);

    public bool IsInFocusOnly { get; set; } = false;

    public Task SetIsInFocusOnly(bool isInFocusOnly) {
        IsInFocusOnly = isInFocusOnly;
        FilterTskModels();
        StateHasChanged(); //Because a collection changed.
        return Task.CompletedTask;
    }

    protected override async Task HandleUpdateAsync(IsActionStatusOnlyFlag flag) => await SetIsActionStatusOnly(flag.CurrentValue);

    public async Task SetIsActionStatusOnly(bool isActionStatusOnly) {
        statuses = isActionStatusOnly ? StatusExtensions.ActionStatuses : StatusExtensions.AllStatuses;
        UpdateColumns();
        await RefreshBoard();
    }

}
