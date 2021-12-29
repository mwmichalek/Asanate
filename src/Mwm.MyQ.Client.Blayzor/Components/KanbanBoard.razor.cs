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
using Mwm.MyQ.Client.Blayzor.Models.Tsks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Fluxor;
using System.Collections;

namespace Mwm.MyQ.Client.Blayzor.Components;

public partial class KanbanBoard : EntityFluxorComponent {

    private SfKanban<TskModel> refKanbanBoard;

    public TskPopup TskPopup;

    [Inject]
    ILogger<KanbanBoard> Logger { get; set; }

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

    [Inject]
    public IActionSubscriber ActionSubscriber { get; set; }

    protected override void OnInitialized() {
        base.OnInitialized();
        UpdateSwimLanes();
        UpdateColumns();
    }

    protected override void BuildTskModels() {
        base.BuildTskModels();

        
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

    public void DialogOpenHandler(DialogOpenEventArgs<TskModel> args) {
        args.Cancel = true;
        Logger.LogInformation("DialogOpenHandler!!!!!");
        TskPopup.Update(args.Data);
    }

    public bool IsGroupedByCompany { get; set; } = true;

    public async Task SetIsGroupedByCompany(bool isGroupedByCompany) {
        IsGroupedByCompany = isGroupedByCompany;
        UpdateSwimLanes();
        await refKanbanBoard.RefreshAsync();
    }

    public bool IsInFocusOnly { get; set; } = false;

    public async Task SetIsInFocusOnly(bool isInFocusOnly) {
        IsInFocusOnly = isInFocusOnly;
        FilterTskModels();
        StateHasChanged(); //Because a collection changed.
    }

    public async Task SetIsActionStatusOnly(bool isActionStatusOnly) {
        statuses = isActionStatusOnly ? StatusExtensions.ActionStatuses : StatusExtensions.AllStatuses;
        UpdateColumns();
        await refKanbanBoard.RefreshAsync();
    }

    public TskModel SelectedTskModel { get; set; }

}
