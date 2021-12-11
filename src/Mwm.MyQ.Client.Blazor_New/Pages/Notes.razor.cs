using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Mwm.MyQ.Client.Blazor.Models.Tsks;
using Mwm.MyQ.Application.Tsks.Commands;
using Mwm.MyQ.Client.Service.Facades;
using System;
using Mwm.MyQ.Domain;
using Mwm.MyQ.Client.Service.Store.State.Shared;
using Fluxor;
using Microsoft.AspNetCore.Components.Web;
using Mwm.MyQ.Client.Service.Store.Features.Shared.Helpers;
using System.Collections.Generic;
using Mwm.MyQ.Client.Blazor.Models.Shared;
using System.Linq;

namespace Mwm.MyQ.Client.Blazor.Pages {
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

        public TskModel NewTskModel { get; set; } = new TskModel();

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
                                                                      .Select(p => p.ToDropDownEntity(index++))
                                                                      .OrderBy(dde => dde.Index)
                                                                      .ToList();

                // If Project hasn't been set yet, then set it to 'Generic' 
                if (selectedProjectId == 0) {
                    var genericProject = ProjectsState.Value.Entities.SingleOrDefault(p => p.Name == Project.DefaultProjectName);
                    if (genericProject != null) {
                        selectedProjectId = genericProject.Id;
                        Logger.LogInformation($"Default Project: {genericProject.Id} {genericProject.Name}");
                    }
                }

                UpdateInitiativeDropDown();
            }
        }

        private void UpdateInitiativeDropDown() {

            var index = 1;
            InitiativeDropDownEntities = InitiativesState.Value.Entities.Where(i => i.ProjectId == selectedProjectId)
                                                                        .OrderBy(i => i.Name)
                                                                        .Select(i => i.ToDropDownEntity(index++))
                                                                        .OrderBy(dde => dde.Index)
                                                                        .ToList();

            // Whenever you change projects default to Triage
            var triageInitiative = InitiativesState.Value.Entities.SingleOrDefault(i => i.ProjectId == selectedProjectId &&
                                                                                        i.Name == Initiative.DefaultInitiativeName);

            if (triageInitiative != null) {
                selectedInitiativeId = triageInitiative.Id;
                Logger.LogInformation($"Default Initiative: {triageInitiative.Id} {triageInitiative.Name}");
            }
        }

        public void Saved(EntityState<Tsk> entityState) {
            if (entityState.CurrentEntity != null &&
                entityState.CurrentEntity.Name == NewTskModel.Name) {
                NewTskModel = new TskModel();
                SavedTskModels.Insert(0, NewTskModel);
            }
        }

        public void Save() {
            try {
                Logger.LogInformation($"Add: {NewTskModel.Name}");

                EntityStateFacade.Add<Tsk, TskAdd.Command>(new TskAdd.Command {
                    Name = NewTskModel.Name,
                    DurationEstimate = NewTskModel.DurationEstimate,
                    InitiativeId = selectedInitiativeId
                });
            } catch (Exception ex) {
                Logger.LogError($"Unable to update: {NewTskModel.Name}, {ex}");
            }
        }

        public void KeyboardEventHandler(KeyboardEventArgs args) {
            if (args.Key == "Enter")
                Save();
        }
    }
}
