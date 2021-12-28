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

public partial class KanbanBoard : FluxorComponent {

    private SfKanban<TskModel> refKanbanBoard;

    public TskPopup TskPopup;

    [Inject]
    ILogger<KanbanBoard> Logger { get; set; }

    [Inject]
    public IState<EntityState<Tsk>> TsksState { get; set; }

    [Inject]
    public IState<EntityState<Initiative>> InitiativesState { get; set; }

    [Inject]
    public IState<EntityState<Company>> CompaniesState { get; set; }

    [Inject]
    public IState<EntityState<Project>> ProjectsState { get; set; }

    [Inject]
    public EntityStateFacade EntityStateFacade { get; set; }

    public bool IsGroupedByCompany { get; set; } = true;

    public bool IsArchivedRemoved { get; set; } = true;

    public bool IsInFocusOnly { get; set; } = false;

    private List<TskModel> tskModels = new List<TskModel>();

    public IEnumerable<TskModel> TskModels {
        get => tskModels;
        set { }
    }

    private List<TskModel> filteredTskModels = new List<TskModel>();

    public IEnumerable<TskModel> FilteredTskModels {
        get => filteredTskModels;
        set { }
    }

    public List<string> Companies => TskModels.Select(t => t.CompanyName).Distinct().ToList();

    [Inject]
    public IActionSubscriber ActionSubscriber { get; set; }

    public bool IsLoading() => TsksState.IsLoading() ||
                               InitiativesState.IsLoading() ||
                               ProjectsState.IsLoading() ||
                               CompaniesState.IsLoading();

    public bool HasErrors() => TsksState.HasErrors() ||
                               InitiativesState.HasErrors() ||
                               ProjectsState.HasErrors() ||
                               CompaniesState.HasErrors();

    public bool HasValues() => TsksState.HasValue(true) &&
                               InitiativesState.HasValue(true) &&
                               ProjectsState.HasValue(true) &&
                               CompaniesState.HasValue(true);

    protected override void OnInitialized() {
        if (!TsksState.HasValue())
            EntityStateFacade.Load<Tsk>();
        if (!InitiativesState.HasValue())
            EntityStateFacade.Load<Initiative>();
        if (!CompaniesState.HasValue())
            EntityStateFacade.Load<Company>();
        if (!ProjectsState.HasValue())
            EntityStateFacade.Load<Project>();

        TsksState.StateChanged += (s, e) => BuildTskModels();
        InitiativesState.StateChanged += (s, e) => BuildTskModels();
        ProjectsState.StateChanged += (s, e) => BuildTskModels();
        CompaniesState.StateChanged += (s, e) => BuildTskModels();
        
        BuildTskModels();
        UpdateSwimLanes();
        base.OnInitialized();
    }

    private void BuildTskModels() {
        var index = 0;
        if (HasValues()) {
            var tsks = TsksState.Value.Entities.Where(t => !t.IsArchived); 

            tskModels = tsks.Select(t => CreateModel(t))
                                .OrderByDescending(tm => tm.IsInFocus)
                                .ThenBy(tm => tm.CompanyName)
                                .ThenBy(tm => tm.ProjectAbbreviation)
                                .ThenBy(tm => tm.InitiativeName)
                                .ToList();

            foreach (var tskModel in tskModels)
                tskModel.RankId = index++;

            Logger.LogInformation($"Built {tskModels.Count} TskModels");

            FilterTskModels();
        }
    }

    private TskModel CreateModel(Tsk t) {
        Initiative initiative = InitiativesState.FindById(t.InitiativeId);
        Project project = (initiative != null) ? ProjectsState.FindById(initiative.ProjectId) : null;
        Company company = (project != null) ? CompaniesState.FindById(project.CompanyId) : null;

        return new TskModel {
            Id = t.Id,
            Name = t.Name,
            Status = t.Status,
            DurationEstimate = t.DurationEstimate,
            DurationCompleted = t.DurationCompleted,
            Notes = t.Notes,
            DueDate = t.DueDate,
            StartDate = t.StartDate,
            StartedDate = t.StartedDate,
            CompletedDate = t.CompletedDate,
            InitiativeName = initiative?.Name,
            InitiativeExternalId = EntityHelpers.ToExternalId(initiative, project),
            InitiativeExternalLink = EntityHelpers.ToExternalLink(initiative, project),
            ProjectName = project?.Name,
            ProjectAbbreviation = project?.Abbreviation,
            CompanyName = company?.Name,
            ProjectColor = project?.Color,
            IsInFocus = t.IsInFocus
        };
    }

    private void FilterTskModels() { 
        var filtered = tskModels as IEnumerable<TskModel>;
        if (IsInFocusOnly)
            filtered = filtered.Where(tm => tm.IsInFocus);
        filteredTskModels = filtered.ToList();
    }

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

    public async Task SetIsGroupedByCompany(bool isGroupedByCompany) {
        IsGroupedByCompany = isGroupedByCompany;
        UpdateSwimLanes();
        StateHasChanged();
        await refKanbanBoard.RefreshAsync();
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

    public async Task SetIsInFocusOnly(bool isInFocusOnly) {
        IsInFocusOnly = isInFocusOnly;
        FilterTskModels();
        StateHasChanged();
        await refKanbanBoard.RefreshAsync();
        
    }

    public TskModel SelectedTskModel { get; set; }

}
