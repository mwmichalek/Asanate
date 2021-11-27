using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Mwm.MyQ.Client.Blazor.Models.Tsks;
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
using Mwm.MyQ.Client.Blazor.Components;

namespace Mwm.MyQ.Client.Blazor.Pages {
    public partial class Kanban : FluxorComponent {

        public SfKanban<TskModel> KanbanBoard;

        public TskPopup TskPopup;

        [Inject]
        ILogger<Kanban> Logger { get; set; }

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

        //private bool rebuildTskModels = false;
        private List<TskModel> tskModels = new List<TskModel>();

        public IEnumerable<TskModel> TskModels {
            get => tskModels;
            set { }
        }

        public List<string> Companies => TskModels.Select(t => t.CompanyName).Distinct().ToList();

        public List<string> StatusNames => TskModels.Select(t => t.Status)
                                                    .Distinct()
                                                    .OrderBy(s => (int)s)
                                                    .Select(s => s.ToStr())
                                                    .ToList();

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

            base.OnInitialized();
        }


        private void BuildTskModels(string triggerBy = null) {
            if (HasValues()) {
                tskModels = TsksState.Value.Entities?.Where(t => !t.IsArchived).Select(t => {
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
                        InitiativeExternalId = initiative?.ExternalId,
                        ProjectName = project?.Name,
                        CompanyName = company?.Name
                    };
                }).ToList();
                Logger.LogInformation($"Built {tskModels.Count} TskModels");
            }
        }

        public void DragStopHandler(DragEventArgs<TskModel> args) {
            foreach (var tskModel in args.Data) {
                try {
                    var tsk = TsksState.FindById(tskModel.Id);

                    Logger.LogInformation($"Moved: {tskModel.Name}, FromStatus: {tsk.Status} ToStatus: {tskModel.Status}");
                    EntityStateFacade.Update<Tsk, TskUpdate.Command>(new TskUpdate.Command {
                        Id = tskModel.Id,
                        Name = tsk.Name,
                        Status = tskModel.Status
                    });
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

        public TskModel SelectedTskModel { get; set; }

    }
}
