using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Mwm.Asanate.Client.Blazor.Models.Tsks;
using Mwm.Asanate.Client.Service.Facades;
using Mwm.Asanate.Client.Service.Store.Features.Shared.Actions;
using Mwm.Asanate.Client.Service.Store.Features.Shared.Helpers;
using Mwm.Asanate.Client.Service.Store.State.Shared;
using Mwm.Asanate.Application.Tsks.Commands;
using Mwm.Asanate.Data;
using Mwm.Asanate.Domain;
using Syncfusion.Blazor.Kanban;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Syncfusion.Blazor.Popups;
using Mwm.Asanate.Client.Blazor.Components;

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
        public EntityStateFacade EntityStateFacade { get; set; }


    }
}
