using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Mwm.MyQ.Client.Blayzor.Models.Tsks;
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
using Mwm.MyQ.Client.Blayzor.Components;
using Mwm.MyQ.Client.Blayzor.Helpers;

namespace Mwm.MyQ.Client.Blayzor.Pages {
    public partial class Kanban : FluxorComponent {

        public KanbanBoard refKanbanBoard;

        private bool _isGroupedByCompany = true;
        public bool IsGroupedByCompany {
            get => _isGroupedByCompany;
            set {
                _isGroupedByCompany = value;
                refKanbanBoard.SetIsGroupedByCompany(_isGroupedByCompany);
            }
        }

        private bool _isArchivedRemoved = true;

        public bool IsArchivedRemoved {
            get => _isArchivedRemoved;
            set {
                _isArchivedRemoved = value;
                refKanbanBoard.SetIsArchivedRemoved(_isArchivedRemoved);
            }
        }


        private bool _isInFocusOnly = false;
        public bool IsInFocusOnly { 
            get => _isInFocusOnly;
            set {
                _isInFocusOnly = value;
                refKanbanBoard.SetIsInFocusOnly(_isInFocusOnly);
            }
}

        //public async Task ToggleIsGroupedByCompany() {
        //    IsGroupedByCompany = !IsGroupedByCompany;
        //    await refKanbanBoard.ToggleIsGroupedByCompany(IsGroupedByCompany);
        //}

        //public async Task ToggleIsArchivedRemoved() {
        //    IsArchivedRemoved = !IsArchivedRemoved;
        //    await refKanbanBoard.ToggleIsGroupedByCompany(IsGroupedByCompany);
        //}

        //public async Task ToggleIsInFocusOnly() {
        //    IsInFocusOnly = !IsInFocusOnly;
        //    await refKanbanBoard.ToggleIsInFocusOnly(IsInFocusOnly);
        //}
    }
}
