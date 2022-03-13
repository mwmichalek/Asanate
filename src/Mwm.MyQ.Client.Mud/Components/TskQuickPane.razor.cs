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

    public Initiative PendingInitiative { get; set; } = new Initiative();
    public string PendingInitiativeExternalIdPrefix { get; set; } = string.Empty;

    public Tsk PendingTsk { get; set; } = new Tsk { Status = Status.Open };

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
            PendingTsk.InitiativeId = selectedInitiative.Id;
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

        TsksState.StateChanged += (s, e) => SavedPendingTsk();
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
            
            // New Initiative triggered rebuild of dropdown
            if (!string.IsNullOrEmpty(PendingInitiative.Name) && 
                InitiativesState.Value.CurrentEntity != null &&
                InitiativesState.Value.CurrentEntity.Name == PendingInitiative.Name) {

                SelectedInitiative = InitiativesState.Value.CurrentEntity;
                PendingInitiativeExternalIdPrefix = string.Empty;
                PendingInitiative = new Initiative();
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
            if (SelectedInitiative == null || SelectedInitiative.ProjectId != selectedProjectId) 
                SelectedInitiative = InitiativesState.Value.Entities.SingleOrDefault(i => i.ProjectId == selectedProjectId &&
                                                                                          i.Name == Initiative.DefaultInitiativeName);

            StateHasChanged();
        }
    }

    public void SetInitiativeCreationMode(bool isInInitiativeCreationMode) {
        IsInInitiativeCreationMode = isInInitiativeCreationMode;
        if (!IsInInitiativeCreationMode) 
            PendingInitiative = new Initiative();
        StateHasChanged();
    }

    public async Task SavePendingInitiativeAsync() {

        // Save to server
        await EntityStateFacade.Add<Initiative, InitiativeAdd.Command>(new InitiativeAdd.Command {
            Name = PendingInitiative.Name,
            ExternalId = !string.IsNullOrEmpty(PendingInitiative.ExternalId) ? PendingInitiative.ExternalId : null,
            ProjectId = selectedProject.Id
        });

        IsInInitiativeCreationMode = false;
        StateHasChanged();
    }

    public async Task SavePendingTskAsync() {

        await EntityStateFacade.Add<Tsk, TskAdd.Command>(new TskAdd.Command {
            Name = PendingTsk.Name,
            DurationEstimate = PendingTsk.DurationEstimate > 0 ? PendingTsk.DurationEstimate : null,
            Status = PendingTsk.Status,
            InitiativeId = PendingTsk.InitiativeId
        });


        //try {

        //    if (!string.IsNullOrEmpty(NewTskName) && !IsInInitiativeCreationMode) {
        //        Logger.LogInformation($"Pending item: {NewTskName}");
        //        float.TryParse(NewTskEstimatedDuration, out float estimatedDuration);
        //        var tskStatus = NewTskStatus.ToStatus();


        //        PendingTskName = NewTskName;
        //        NewTskName = string.Empty;
        //        NewTskEstimatedDuration = string.Empty;
        //        StateHasChanged();
        //        //await refTskName.FocusAsync();
        //    } else if (!string.IsNullOrEmpty(NewInitiativeName) && IsInInitiativeCreationMode) {
        //        Logger.LogInformation($"Pending item: {NewInitiativeName}");


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

    public void ResetPendingTsk() {
        PendingTsk = new Tsk {
            InitiativeId = PendingTsk.InitiativeId,
            Status = PendingTsk.Status
        };
    }

    public void SavedPendingTsk() {
        if (TsksState.Value.CurrentEntity != null &&
            TsksState.Value.CurrentEntity.Name == PendingTsk.Name)
            ResetPendingTsk();
    }

}