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

        public List<DropDownEntity> ProjectDropDownEntities { get; set; } = new List<DropDownEntity>();

        private int selectedProjectId;
        public int SelectedProjectId {
            get { return selectedProjectId; }
            set {
                selectedProjectId = value;
                UpdateInitiativeDropDown();
            }
        }

        public List<DropDownEntity> InitiativeDropDownEntities { get; set; } = new List<DropDownEntity>();

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

            base.OnInitialized();
        }

        private void UpdateInitiativeDropDown() {
            InitiativeDropDownEntities = InitiativesState.Value.Entities.Where(i => i.ProjectId == selectedProjectId)
                                                                        .Select(i => new DropDownEntity { Id = i.Id, Name = i.Name })
                                                                        .ToList();
        }

        private void UpdateProjectDropDown() {
            ProjectDropDownEntities = ProjectsState.Value.Entities.Select(p => new DropDownEntity { Id = p.Id, Name = p.Name })
                                                                  .ToList();
        }

        public void Saved(EntityState<Tsk> entityState) {
            if (entityState.CurrentEntity.Name == NewTskModel.Name) {
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
                    DurationEstimate = NewTskModel.DurationEstimate
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
