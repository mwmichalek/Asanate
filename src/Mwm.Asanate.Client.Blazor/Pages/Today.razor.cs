using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Mwm.Asanate.Client.Blazor.Models.Tsks;
using Mwm.Asanate.Client.Blazor.Services;
using Mwm.Asanate.Client.Blazor.Services.State;
using Mwm.Asanate.Client.Blazor.Store.Features.Shared.Actions;
using Mwm.Asanate.Client.Blazor.Store.Features.Shared.Helpers;
using Mwm.Asanate.Client.Blazor.Store.State.Shared;
using Mwm.Asanate.Data;
using Mwm.Asanate.Domain;
using Syncfusion.Blazor.Kanban;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mwm.Asanate.Client.Blazor.Pages {
    public partial class Today : FluxorComponent {

        public SfKanban<TskModel> KanbanBoard;

        [Inject]
        ILogger<Today> Logger { get; set; }

        [Inject]
        public IState<EntityState<Tsk>> TsksState { get; set; }

        [Inject]
        public IState<EntityState<Initiative>> InitiativesState { get; set; }

        [Inject]
        public IState<EntityState<Company>> CompaniesState { get; set; }

        [Inject]
        public IState<EntityState<Project>> ProjectsState { get; set; }

        [Inject]
        public EntityStateService EntityService { get; set; }

        private bool rebuildTskModels = false;
        private List<TskModel> tskModels = new List<TskModel>();

        public List<TskModel> TskModels {
            get {
                if (rebuildTskModels) 
                    BuildTskModels();
                return tskModels;
            }
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
                EntityService.Load<Tsk>();
            if (!InitiativesState.HasValue())
                EntityService.Load<Initiative>();
            if (!CompaniesState.HasValue())
                EntityService.Load<Company>();
            if (!ProjectsState.HasValue())
                EntityService.Load<Project>();

            //TODO:(MWM) Perhaps Make the Entities Lists a Lookup for fast access.

            //NOTE:(MWM) These actions get triggered BEFORE the state gets updated.  WTF?!?!?
            ActionSubscriber.SubscribeToAction<LoadSuccessAction<Tsk>>(this, (action) => rebuildTskModels = true);
            ActionSubscriber.SubscribeToAction<LoadSuccessAction<Initiative>>(this, (action) => rebuildTskModels = true);
            ActionSubscriber.SubscribeToAction<LoadSuccessAction<Project>>(this, (action) => rebuildTskModels = true);
            ActionSubscriber.SubscribeToAction<LoadSuccessAction<Company>>(this, (action) => rebuildTskModels = true);
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
                        InitiativeName = initiative?.Name,
                        ProjectName = project?.Name,
                        CompanyName = company?.Name
                    };
                }).ToList();
                rebuildTskModels = false;
                Logger.LogInformation($"Built {TskModels.Count} TskModels");
            }
        }

        public void ActionCompleteHandler(ActionEventArgs<TskModel> args) {
            Logger.LogInformation("Hi");
            Logger.LogInformation($"ChangedRecords: {args.ChangedRecords?.Count()}, EventName: {args.RequestType}");
        }

        public void DragStopHandler(DragEventArgs<TskModel> args) {
            foreach (var tskModel in args.Data) {
                Logger.LogInformation($"Moved: {tskModel.Name}, Status: {tskModel.Status}");
                //EntityService.

            }
            
        }

        //
        //// Summary:
        ////     Returns the appropriate added data based on the action.
        //public IEnumerable<TValue> AddedRecords { get; set; }
        ////
        //// Summary:
        ////     Defines the cancel option for the action taking place.
        //public bool Cancel { get; set; }
        ////
        //// Summary:
        ////     Returns the appropriate changed data based on the action.
        //public IEnumerable<TValue> ChangedRecords { get; set; }
        ////
        //// Summary:
        ////     Returns the appropriate deleted data based on the action.
        //public IEnumerable<TValue> DeletedRecords { get; set; }
        ////
        //// Summary:
        ////     Specifies the name of the event.
        //[Obsolete("This event argument is deprecated and will no longer be used.")]
        //public string Name { get; set; }
        ////
        //// Summary:
        ////     Returns the request type of the current action.
        //public string RequestType { get; set; }
        ////
        //// Summary:
        ////     Returns the failure exceptions.
        //public Exception Exception { get; set; }
    }
}
