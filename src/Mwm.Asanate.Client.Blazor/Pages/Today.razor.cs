using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Mwm.Asanate.Client.Blazor.Models.Tsks;
using Mwm.Asanate.Client.Blazor.Services;
using Mwm.Asanate.Client.Blazor.Services.State;
using Mwm.Asanate.Client.Blazor.Store.Features.Shared.Actions;
using Mwm.Asanate.Client.Blazor.Store.State.Shared;
using Mwm.Asanate.Data;
using Mwm.Asanate.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mwm.Asanate.Client.Blazor.Pages {
    public partial class Today : FluxorComponent {

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

        public List<TskModel> TskModels => BuildTskModels();

        [Inject]
        public IActionSubscriber ActionSubscriber { get; set; }

        protected override void OnInitialized() {
            if (TsksState.Value.Entities is null) 
                EntityService.Load<Tsk>();
            if (InitiativesState.Value.Entities is null)
                EntityService.Load<Initiative>();
            if (CompaniesState.Value.Entities is null)
                EntityService.Load<Company>();
            if (ProjectsState.Value.Entities is null)
                EntityService.Load<Project>();

            //TODO:(MWM) Perhaps Make the Entities Lists a Lookup for fast access.

            //ActionSubscriber.SubscribeToAction<LoadSuccessAction<Tsk>>(this, (action) => LoadModels());
            base.OnInitialized();
        }

        private List<TskModel> BuildTskModels() {
            return TsksState.Value.Entities.Select(t => {
                Initiative initiative = InitiativesState.Value.Entities.SingleOrDefault(i => i.Id == t.InitiativeId);
                Project project = (initiative != null) ? ProjectsState.Value.Entities.SingleOrDefault(p => p.Id == initiative.ProjectId) : null;
                Company company = (project != null) ? CompaniesState.Value.Entities.SingleOrDefault(c => c.Id == project.CompanyId) : null;

                return new TskModel {
                    Id = t.Id,
                    Name = t.Name,
                    InitiativeName = initiative?.Name,
                    ProjectName = project?.Name,
                    CompanyName = company?.Name
                };
            }).ToList();
        }
    }
}
