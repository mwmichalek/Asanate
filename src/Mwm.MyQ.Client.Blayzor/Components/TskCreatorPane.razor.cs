using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Logging;
using Mwm.MyQ.Application.Initiatives.Commands;
using Mwm.MyQ.Application.Tsks.Commands;
using Mwm.MyQ.Client.Blayzor.Models.Shared;
using Mwm.MyQ.Client.Service.Models;
using Mwm.MyQ.Client.Service.Facades;
using Mwm.MyQ.Client.Service.Store.Features.Shared.Helpers;
using Mwm.MyQ.Client.Service.Store.State.Shared;
using Mwm.MyQ.Domain;
using Syncfusion.Blazor.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mwm.MyQ.Client.Service.Components;

namespace Mwm.MyQ.Client.Blayzor.Components;

public partial class TskCreatorPane : FluxorComponent {

    [Inject]
    ILogger<TskCreatorPane> Logger { get; set; }

    [Inject]
    public IState<EntityState<Tsk>> TsksState { get; set; }

    [Inject]
    public IState<EntityState<Initiative>> InitiativesState { get; set; }

    [Inject]
    public IState<EntityState<Project>> ProjectsState { get; set; }

    [Inject]
    public IState<EntityState<Company>> CompaniesState { get; set; }

    [Inject]
    public EntityStateFacade EntityStateFacade { get; set; }

    private SfTextBox refTskName;

    private SfTextBox refTskEstimatedDuration;

    private SfTextBox refInitiativeName;

    public string NewTskName { get; set; } = string.Empty;

    public string PendingTskName { get; set; } = string.Empty;

    public string NewTskEstimatedDuration { get; set; }

    public string NewTskStatus { get; set; } = Status.Open.ToStr();

    public string NewInitiativeName { get; set; } = string.Empty;

    public string PendingInitiativeName { get; set; } = string.Empty;

    public string NewInitiativeExternalId { get; set; } = string.Empty;

    public string NewInitiativeExternalIdPrefix { get; set; } = string.Empty;

    public List<TskModel> SavedTskModels { get; set; } = new List<TskModel>();

    public bool IsInInitiativeCreationMode { get; set; } = false;

    public IEnumerable<DropDownEntity> ProjectDropDownEntities { get; set; } = new List<DropDownEntity>();

    private int selectedProjectId = 0;
    public int SelectedProjectId {
        get { return selectedProjectId; }
        set {
            selectedProjectId = value;
            UpdateInitiativeDropDown();
        }
    }

    private int selectedInitiativeId = 0;
    public int SelectedInitiativeId {
        get { return selectedInitiativeId; }
        set {
            selectedInitiativeId = value;
        }
    }

    public IEnumerable<DropDownEntity> InitiativeDropDownEntities { get; set; } = new List<DropDownEntity>();

    protected override async Task OnInitializedAsync() {
        await base.OnInitializedAsync();

        if (!TsksState.HasValue())
            await EntityStateFacade.Load<Tsk>();
        if (!InitiativesState.HasValue())
            await EntityStateFacade.Load<Initiative>();
        if (!CompaniesState.HasValue())
            await EntityStateFacade.Load<Company>();
        if (!ProjectsState.HasValue())
            await EntityStateFacade.Load<Project>();

        TsksState.StateChanged += (s, e) => Saved(e);
        InitiativesState.StateChanged += (s, e) => UpdateInitiativeDropDown();
        ProjectsState.StateChanged += (s, e) => UpdateProjectDropDown();
        UpdateProjectDropDown();
    }

    private void UpdateProjectDropDown() {
        if (ProjectsState.HasValue()) {
            var index = 1;
            ProjectDropDownEntities = ProjectsState.Value.Entities.OrderBy(p => p.Name)
                                                                  .Select(p => p.ToDropDownEntity(CompaniesState, index++))
                                                                  .OrderBy(dde => dde.Index)
                                                                  .ToList();

            // If Project hasn't been set yet, then set it to 'Generic' 
            if (selectedProjectId == 0) {
                var genericProject = ProjectsState.Value.Entities.SingleOrDefault(p => p.Name == Project.DefaultProjectName);
                if (genericProject != null)
                    selectedProjectId = genericProject.Id;
            }

            UpdateInitiativeDropDown();
        }
    }

    private void UpdateInitiativeDropDown() {

        if (InitiativesState.HasValue()) {
            Logger.LogInformation("Updating Initiatives Dropdown.");
            Project project = ProjectsState.FindById(selectedProjectId);
            NewInitiativeExternalIdPrefix = (project != null && !string.IsNullOrEmpty(project.ExternalIdPrexfix)) ?
                $"{project.ExternalIdPrexfix}-" :
                string.Empty;

            var index = 1;
            InitiativeDropDownEntities = InitiativesState.Value.Entities.Where(i => i.ProjectId == selectedProjectId)
                                                                        .OrderBy(i => i.Name)
                                                                        .Select(i => i.ToDropDownEntity(index++))
                                                                        .OrderBy(dde => dde.Index)
                                                                        .ToList();
            var selectInitiativeName = (!string.IsNullOrEmpty(PendingInitiativeName)) ?
                PendingInitiativeName : Initiative.DefaultInitiativeName;

            // Whenever you change projects default to Triage
            var selectInitiative = InitiativesState.Value.Entities.SingleOrDefault(i => i.ProjectId == selectedProjectId &&
                                                                                        i.Name == selectInitiativeName);
            if (selectInitiative != null) {
                Logger.LogInformation($"Setting default Initiative to {selectInitiative.Name}");
                selectedInitiativeId = selectInitiative.Id;
            }

        }
    }

    public async Task SetInitiativeCreationMode(bool isInInitiativeCreationMode) {
        this.IsInInitiativeCreationMode = isInInitiativeCreationMode;
        if (!isInInitiativeCreationMode) {
            NewInitiativeName = string.Empty;
            NewInitiativeExternalId = string.Empty;
        }

        StateHasChanged();

        if (isInInitiativeCreationMode) {
            if (refInitiativeName == null)
                await Task.Delay(1000);
            await refInitiativeName?.FocusAsync();
        }
    }

    public async Task Saving() {
        try {

            if (!string.IsNullOrEmpty(NewTskName) && !IsInInitiativeCreationMode) {
                Logger.LogInformation($"Pending item: {NewTskName}");
                float.TryParse(NewTskEstimatedDuration, out float estimatedDuration);
                var tskStatus = NewTskStatus.ToStatus();

                await EntityStateFacade.Add<Tsk, TskAdd.Command>(new TskAdd.Command {
                    Name = NewTskName,
                    DurationEstimate = estimatedDuration > 0 ? estimatedDuration : null,
                    Status = tskStatus,
                    InitiativeId = selectedInitiativeId
                });
                PendingTskName = NewTskName;
                NewTskName = string.Empty;
                NewTskEstimatedDuration = string.Empty;
                StateHasChanged();
                await refTskName.FocusAsync();
            } else if (!string.IsNullOrEmpty(NewInitiativeName) && IsInInitiativeCreationMode) {
                Logger.LogInformation($"Pending item: {NewInitiativeName}");

                await EntityStateFacade.Add<Initiative, Application.Initiatives.Commands.InitiativeAdd.Command>(new InitiativeAdd.Command {
                    Name = NewInitiativeName,
                    ExternalId = NewInitiativeExternalId,
                    ProjectId = SelectedProjectId
                });
                PendingInitiativeName = NewInitiativeName;
                NewInitiativeName = string.Empty;
                NewInitiativeExternalId = string.Empty;
                IsInInitiativeCreationMode = false;
                StateHasChanged();
                await refTskName.FocusAsync();
            }
        } catch (Exception ex) {
            Logger.LogError($"Unable to update: {NewTskName}, {ex}");
        }
    }

    public void Saved(EntityState<Tsk> entityState) {
        if (entityState.CurrentEntity != null &&
            entityState.CurrentEntity.Name == PendingTskName) {
            Logger.LogInformation($"Adding item to list: {PendingTskName}");

            var tsk = entityState.CurrentEntity;

            Initiative initiative = InitiativesState.FindById(tsk.InitiativeId);
            Project project = (initiative != null) ? ProjectsState.FindById(initiative.ProjectId) : null;
            Company company = (project != null) ? CompaniesState.FindById(project.CompanyId) : null;

            SavedTskModels.Insert(0, new TskModel {
                Name = tsk.Name,
                InitiativeName = initiative.Name,
                ProjectName = project.Name,
                CompanyName = company.Name,
                DurationEstimate = tsk.DurationEstimate,
                Status = tsk.Status
            });
            PendingTskName = string.Empty;
        }
    }

    private void TskNameChanged(InputEventArgs args) => NewTskName = args.Value;

    private void TskDurationEstimateChanged(InputEventArgs args) => NewTskEstimatedDuration = args.Value;

    private void InitiativeNameChanged(InputEventArgs args) => NewInitiativeName = args.Value;

    private void NewInitiativeExternalIdChanged(InputEventArgs args) => NewInitiativeExternalId = args.Value;

    public async Task KeyboardEventHandler(KeyboardEventArgs args) {
        if (args.Key == "Enter")
            await Saving();
        return;
    }
}