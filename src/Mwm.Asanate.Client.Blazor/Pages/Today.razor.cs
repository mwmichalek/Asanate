using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using Mwm.Asanate.Client.Blazor.Services;
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
        public IState<EntityState<Tsk>> TsksState { get; set; }

        [Inject]
        public EntityService EntityService { get; set; }

        protected override void OnInitialized() {
            if (TsksState.Value.Entities is null) {
                EntityService.Load<Tsk>();
            }

            base.OnInitialized();
        }
    }
}
