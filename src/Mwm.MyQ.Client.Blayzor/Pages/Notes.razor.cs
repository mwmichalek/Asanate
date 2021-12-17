using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Mwm.MyQ.Client.Blayzor.Models.Tsks;
using Mwm.MyQ.Application.Tsks.Commands;
using Mwm.MyQ.Client.Service.Facades;
using System;
using Mwm.MyQ.Domain;
using Mwm.MyQ.Client.Service.Store.State.Shared;
using Fluxor;
using Microsoft.AspNetCore.Components.Web;
using Mwm.MyQ.Client.Service.Store.Features.Shared.Helpers;
using System.Collections.Generic;
using Mwm.MyQ.Client.Blayzor.Models.Shared;
using System.Linq;
using System.Threading.Tasks;
using Syncfusion.Blazor.Inputs;

namespace Mwm.MyQ.Client.Blayzor.Pages {
    public partial class Notes : FluxorComponent {

        [Inject]
        ILogger<Notes> Logger { get; set; }

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

        public string NewTskName { get; set; } = string.Empty;

        public string PendingTskName { get; set; } = string.Empty;

        public string NewTskEstimatedDuration { get; set; }

        public string NewTskStatus { get; set; } = Status.Open.ToStr();

        public List<TskModel> SavedTskModels { get; set; } = new List<TskModel>();

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

        protected override void OnInitialized() {
            if (!TsksState.HasValue())
                EntityStateFacade.Load<Tsk>();
            if (!InitiativesState.HasValue())
                EntityStateFacade.Load<Initiative>();
            if (!CompaniesState.HasValue())
                EntityStateFacade.Load<Company>();
            if (!ProjectsState.HasValue())
                EntityStateFacade.Load<Project>();

            TsksState.StateChanged += (s, e) => Saved(e);
            InitiativesState.StateChanged += (s, e) => UpdateInitiativeDropDown();
            ProjectsState.StateChanged += (s, e) => UpdateProjectDropDown();
            UpdateProjectDropDown();

            base.OnInitialized();
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
                var index = 1;
                InitiativeDropDownEntities = InitiativesState.Value.Entities.Where(i => i.ProjectId == selectedProjectId)
                                                                            .OrderBy(i => i.Name)
                                                                            .Select(i => i.ToDropDownEntity(index++))
                                                                            .OrderBy(dde => dde.Index)
                                                                            .ToList();

                // Whenever you change projects default to Triage
                var triageInitiative = InitiativesState.Value.Entities.SingleOrDefault(i => i.ProjectId == selectedProjectId &&
                                                                                            i.Name == Initiative.DefaultInitiativeName);

                if (triageInitiative != null)
                    selectedInitiativeId = triageInitiative.Id;
            }
        }

        

        public async Task Saving() {
            try {
                if (!string.IsNullOrEmpty(NewTskName)) {
                    Logger.LogInformation($"Pending item: {NewTskName}");
                    float.TryParse(NewTskEstimatedDuration, out float estimatedDuration);
                    var tskStatus = NewTskStatus.ToStatus();

                    EntityStateFacade.Add<Tsk, TskAdd.Command>(new TskAdd.Command {
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
                }
            } catch (Exception ex) {
                Logger.LogError($"Unable to update: {NewTskName}, {ex}");
            }
        }

        private void TskNameChanged(InputEventArgs args) => NewTskName = args.Value;

        private void TskDurationEstimateChanged(InputEventArgs args) => NewTskEstimatedDuration = args.Value;

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

        public async Task KeyboardEventHandler(KeyboardEventArgs args) {
            if (args.Key == "Enter")
                await Saving();
            return;
        }
    }
}
