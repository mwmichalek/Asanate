using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Mwm.Asanate.Client.Blazor.Models.Tsks;
using Mwm.Asanate.Application.Tsks.Commands;
using Mwm.Asanate.Client.Service.Facades;
using System;
using Mwm.Asanate.Domain;
using Mwm.Asanate.Client.Service.Store.State.Shared;
using Fluxor;
using Microsoft.AspNetCore.Components.Web;
using Mwm.Asanate.Client.Service.Store.Features.Shared.Helpers;
using System.Collections.Generic;
using Mwm.Asanate.Client.Blazor.Models.Shared;
using System.Linq;

namespace Mwm.Asanate.Client.Blazor.Pages {
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
            UpdateInitiativeDropDown();

            base.OnInitialized();
        }

        

        private void UpdateProjectDropDown() {
            if (ProjectsState.HasValue()) {
                if (selectedProjectId == 0) {
                    var genericProject = ProjectsState.Value.Entities.SingleOrDefault(p => p.Name == "Generic");
                    if (genericProject != null) {
                        selectedProjectId = genericProject.Id;
                        Logger.LogInformation($"Default Project: {genericProject.Id} {genericProject.Name}");
                    }
                }

                ProjectDropDownEntities = ProjectsState.Value.Entities.Select(p => new DropDownEntity { Id = p.Id, Name = p.Name })
                                                                      .ToList();
            }
        }

        private void UpdateInitiativeDropDown() {
            if (selectedProjectId != 0 && InitiativesState.HasValue()) {
                if (selectedInitiativeId == 0) { 
                    var triageInitiative = InitiativesState.Value.Entities.SingleOrDefault(i => i.ProjectId == selectedProjectId &&
                                                                                           i.Name == "Triage");
                    if (triageInitiative != null) {
                        selectedInitiativeId = triageInitiative.Id;
                        Logger.LogInformation($"Default Initiative: {triageInitiative.Id} {triageInitiative.Name}");
                    }
                }

                InitiativeDropDownEntities = InitiativesState.Value.Entities.Where(i => i.ProjectId == selectedProjectId)
                                                                            .Select(i => new DropDownEntity { Id = i.Id, Name = i.Name })
                                                                            .ToList();
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
            Logger.LogInformation($"Saving Popup.");
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
