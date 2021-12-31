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
using Mwm.MyQ.Client.Service.Store.Features.Settings;

namespace Mwm.MyQ.Client.Blayzor.Pages {
    public partial class Kanban : FluxorComponent {

        [Inject]
        public ApplicationStateFacade ApplicationStateFacade { get; set; }

        public KanbanBoard refKanbanBoard;

        private bool _isGroupedTogether = true;
        public bool IsGroupedTogether {
            get => _isGroupedTogether;
            set {
                ApplicationStateFacade.Set(new IsInFocusOnlyTskFilter {
                    PreviousValue = _isGroupedTogether,
                    CurrentValue = value
                });
                _isGroupedTogether = value;

                //refKanbanBoard.SetIsGroupedByCompany(!_isGroupedTogether).Wait();
            }
        }

        private bool _isInFocusOnly = false;
        public bool IsInFocusOnly { 
            get => _isInFocusOnly;
            set {
                ApplicationStateFacade.Set(new IsGroupedByCompanyFlag {
                    PreviousValue = _isInFocusOnly,
                    CurrentValue = value
                });
                _isInFocusOnly = value;

                //refKanbanBoard.SetIsInFocusOnly(_isInFocusOnly).Wait();
            }
        }

        private bool _isActionStatusOnly = false;
        public bool IsActionStatusOnly {
            get => _isActionStatusOnly;
            set {
                ApplicationStateFacade.Set(new IsActionStatusOnlyFlag {
                    PreviousValue = _isActionStatusOnly,
                    CurrentValue = value
                });
                _isActionStatusOnly = value;

                //refKanbanBoard.SetIsActionStatusOnly(_isActionStatusOnly).Wait();
            }
        }

    }
}