using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Mwm.MyQ.Client.Service.Facades;
using Mwm.MyQ.Client.Service.Store.Features.Shared.Helpers;
using Mwm.MyQ.Client.Service.Store.State.Shared;
using Mwm.MyQ.Domain;
using Mwm.MyQ.Application.Tsks.Commands;
using Mwm.MyQ.Application.Initiatives.Commands;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mwm.MyQ.Client.Service.Models;
using Mwm.MyQ.Client.Mud.Models.Shared;
using System;

namespace Mwm.MyQ.Client.Mud.Components;

public partial class TskQuickPane : FluxorComponent {

    [Inject]
    ILogger<TskQuickPane> Logger { get; set; }

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

    //public string NewTskName { get; set; } = string.Empty;

    //public string PendingTskName { get; set; } = string.Empty;

    //public string NewTskEstimatedDuration { get; set; }

    //public string NewTskStatus { get; set; } = Status.Open.ToStr();

    //public string NewInitiativeName { get; set; } = string.Empty;

    //public string PendingInitiativeName { get; set; } = string.Empty;

    //public string NewInitiativeExternalId { get; set; } = string.Empty;

    

    //public List<TskModel> SavedTskModels { get; set; } = new List<TskModel>();

    public bool IsInInitiativeCreationMode { get; set; } = false;

    public string PendingInitiativeExternalIdPrefix { get; set; } = string.Empty;

    public string PendingInitiativeName { get; set; } = string.Empty;

    public string PendingInitiativeExternalId { get; set; } = string.Empty;

    public List<Project> Projects { get; set; } = new List<Project>();

    private Project selectedProject;

    public Project SelectedProject {
        get => selectedProject; 
        set {
            selectedProject = value;
            UpdateInitiativeDropDown();
        }
    }

    public List<Initiative> Initiatives { get; set; } = new List<Initiative>();

    private Initiative selectedInitiative;

    public Initiative SelectedInitiative {
        get => selectedInitiative; 
        set {
            selectedInitiative = value;
        }
    }

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
            Projects = ProjectsState.Value.Entities.OrderBy(p => p.Name).ToList();

            // If Project hasn't been set yet, then set it to 'Generic' 
            if (selectedProject == null) {
                var genericProject = ProjectsState.Value.Entities.SingleOrDefault(p => p.Name == Project.DefaultProjectName);
                if (genericProject != null) 
                    selectedProject = genericProject;
            }
            IsInInitiativeCreationMode = false;
            UpdateInitiativeDropDown();
        }
    }

    private void UpdateInitiativeDropDown() {

        if (InitiativesState.HasValue()) {
            Logger.LogInformation($"DropDown Build: {PendingInitiativeName} [{InitiativesState.Value.CurrentEntity}]");
            // New Initiative triggered rebuild of dropdown
            if (!string.IsNullOrEmpty(PendingInitiativeName) && 
                InitiativesState.Value.CurrentEntity != null &&
                InitiativesState.Value.CurrentEntity.Name == PendingInitiativeName) {

                selectedInitiative = InitiativesState.Value.CurrentEntity;
                PendingInitiativeExternalIdPrefix = string.Empty;
                PendingInitiativeName = string.Empty;
                PendingInitiativeExternalId = string.Empty;
            }

            Logger.LogInformation("Updating Initiatives Dropdown.");
            var selectedProjectId = selectedProject.Id;
            Project project = ProjectsState.FindById(selectedProjectId);
            PendingInitiativeExternalIdPrefix = (project != null && !string.IsNullOrEmpty(project.ExternalIdPrexfix)) ?
                $"{project.ExternalIdPrexfix}-" :
                string.Empty;

            Initiatives = InitiativesState.Value.Entities.Where(i => i.ProjectId == selectedProjectId)
                                                         .OrderBy(i => i.Name)
                                                         .ToList();

            // If initiative is not set or from a different project, update that shit
            if (selectedInitiative == null || selectedInitiative.ProjectId != selectedProjectId) 
                selectedInitiative = InitiativesState.Value.Entities.SingleOrDefault(i => i.ProjectId == selectedProjectId &&
                                                                                          i.Name == Initiative.DefaultInitiativeName);

            StateHasChanged();
        }
    }

    public void SetInitiativeCreationMode(bool isInInitiativeCreationMode) {
        IsInInitiativeCreationMode = isInInitiativeCreationMode;
        StateHasChanged();
    }

    public async Task SavePendingInitiative() {

        // Save to server
        await EntityStateFacade.Add<Initiative, InitiativeAdd.Command>(new InitiativeAdd.Command {
            Name = PendingInitiativeName,
            ExternalId = !string.IsNullOrEmpty(PendingInitiativeExternalId) ? PendingInitiativeExternalId : null,
            ProjectId = selectedProject.Id
        });

        IsInInitiativeCreationMode = false;
        StateHasChanged();
    }

    public async Task Saving() {
        //try {

        //    if (!string.IsNullOrEmpty(NewTskName) && !IsInInitiativeCreationMode) {
        //        Logger.LogInformation($"Pending item: {NewTskName}");
        //        float.TryParse(NewTskEstimatedDuration, out float estimatedDuration);
        //        var tskStatus = NewTskStatus.ToStatus();

        //        await EntityStateFacade.Add<Tsk, TskAdd.Command>(new TskAdd.Command {
        //            Name = NewTskName,
        //            DurationEstimate = estimatedDuration > 0 ? estimatedDuration : null,
        //            Status = tskStatus,
        //            InitiativeId = selectedInitiative.Id
        //        });
        //        PendingTskName = NewTskName;
        //        NewTskName = string.Empty;
        //        NewTskEstimatedDuration = string.Empty;
        //        StateHasChanged();
        //        //await refTskName.FocusAsync();
        //    } else if (!string.IsNullOrEmpty(NewInitiativeName) && IsInInitiativeCreationMode) {
        //        Logger.LogInformation($"Pending item: {NewInitiativeName}");

        //        await EntityStateFacade.Add<Initiative, InitiativeAdd.Command>(new InitiativeAdd.Command {
        //            Name = NewInitiativeName,
        //            ExternalId = NewInitiativeExternalId,
        //            ProjectId = selectedInitiative.Id
        //        });
        //        PendingInitiativeName = NewInitiativeName;
        //        NewInitiativeName = string.Empty;
        //        NewInitiativeExternalId = string.Empty;
        //        IsInInitiativeCreationMode = false;
        //        StateHasChanged();
        //        //await refTskName.FocusAsync();
        //    }
        //} catch (Exception ex) {
        //    Logger.LogError($"Unable to update: {NewTskName}, {ex}");
        //}
    }

    public void Saved(EntityState<Tsk> entityState) {
        //if (entityState.CurrentEntity != null &&
        //    entityState.CurrentEntity.Name == PendingTskName) {
        //    Logger.LogInformation($"Adding item to list: {PendingTskName}");

        //    var tsk = entityState.CurrentEntity;

        //    Initiative initiative = InitiativesState.FindById(tsk.InitiativeId);
        //    Project project = (initiative != null) ? ProjectsState.FindById(initiative.ProjectId) : null;
        //    Company company = (project != null) ? CompaniesState.FindById(project.CompanyId) : null;

        //    SavedTskModels.Insert(0, new TskModel {
        //        Name = tsk.Name,
        //        InitiativeName = initiative.Name,
        //        ProjectName = project.Name,
        //        CompanyName = company.Name,
        //        DurationEstimate = tsk.DurationEstimate,
        //        Status = tsk.Status
        //    });
        //    PendingTskName = string.Empty;
        //}
    }

    //private void TskNameChanged(InputEventArgs args) => NewTskName = args.Value;

    //private void TskDurationEstimateChanged(InputEventArgs args) => NewTskEstimatedDuration = args.Value;

    //private void InitiativeNameChanged(InputEventArgs args) => NewInitiativeName = args.Value;

    //private void NewInitiativeExternalIdChanged(InputEventArgs args) => NewInitiativeExternalId = args.Value;

    //public async Task KeyboardEventHandler(KeyboardEventArgs args) {
    //    if (args.Key == "Enter")
    //        await Saving();
    //    return;
    //}
}
