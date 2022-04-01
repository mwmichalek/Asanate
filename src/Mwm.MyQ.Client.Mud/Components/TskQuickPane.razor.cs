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
using Mwm.MyQ.Client.Service.Store.Features.Settings;
using Mwm.MyQ.Client.Service.Components;

namespace Mwm.MyQ.Client.Mud.Components;

public partial class TskQuickPane : EventListenerComponent,
                                    IApplicationSettingConsumer<IsTskQuickPaneVisibleFlag> {
    [Inject]
    public ApplicationStateFacade ApplicationStateFacade { get; set; }

    [Inject]
    public EntityStateFacade EntityStateFacade { get; set; }

    [Inject]
    public IState<EntityState<Tsk>> TsksState { get; set; }

    [Inject]
    public IState<EntityState<Initiative>> InitiativesState { get; set; }

    [Inject]
    public IState<EntityState<Project>> ProjectsState { get; set; }

    [Inject]
    public IState<EntityState<Company>> CompaniesState { get; set; }

    public bool IsTskQuickPaneVisible { get; set;}

    public string PaneClasses => IsTskQuickPaneVisible ? "d-inline" : "d-none";

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

        TsksState.StateChanged += (s, e) => SavedPendingTsk();
        InitiativesState.StateChanged += (s, e) => UpdateInitiativeDropDown();
        ProjectsState.StateChanged += (s, e) => UpdateProjectDropDown();
        UpdateProjectDropDown();
    }

    private void UpdateProjectDropDown() {
        if (ProjectsState.HasValue() && CompaniesState.HasValue()) {
            var companies = CompaniesState.Value.Entities.OrderBy(c => c.SortIndex).ToList();
            Projects.Clear();
            foreach (var company in companies)
                Projects.AddRange(ProjectsState.Value.Entities.Where(p => p.CompanyId == company.Id).OrderBy(p => p.Name).ToList());
   
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

        if (InitiativesState.HasValue() && selectedProject != null) {
            
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
        Logger.LogInformation($"Saving Initiative: {PendingInitiative.Name} ...");
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
        Logger.LogInformation($"Saving Tsk: {PendingTsk.Name} ...");
        await EntityStateFacade.Add<Tsk, TskAdd.Command>(new TskAdd.Command {
            Name = PendingTsk.Name,
            DurationEstimate = PendingTsk.DurationEstimate > 0 ? PendingTsk.DurationEstimate : null,
            Status = PendingTsk.Status,
            InitiativeId = PendingTsk.InitiativeId
        });
    }

    public async Task CloseAsync() {
        ApplicationStateFacade.Set(new IsTskQuickPaneVisibleFlag {
            PreviousValue = true,
            CurrentValue = false
        });
    }

    public void ResetPendingTsk() {
        PendingTsk = new Tsk {
            InitiativeId = PendingTsk.InitiativeId,
            Status = PendingTsk.Status
        };
    }

    public void SavedPendingTsk() {
        if (TsksState.Value.CurrentEntity != null &&
            TsksState.Value.CurrentEntity.Name == PendingTsk.Name) {
            Logger.LogInformation($"... Saved.");
            ResetPendingTsk();
        }
    }

    public Task ApplySetting(IsTskQuickPaneVisibleFlag applicationSetting) {
        IsTskQuickPaneVisible = applicationSetting.CurrentValue;
        return Task.CompletedTask;
    }

    public string ToProjectName(Project project) {
        if (CompaniesState.HasValue()) {
            var company = CompaniesState.FindById(project.CompanyId);
            if (company != null) return $"{company.Name} - {project.Name}";
        }
        return project.Name;
    }
}