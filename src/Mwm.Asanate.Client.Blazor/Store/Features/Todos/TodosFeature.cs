using Fluxor;
using Mwm.Asanate.Client.Blazor.Store.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mwm.Asanate.Client.Blazor.Store.Features.Todos {
    public class TodosFeature : Feature<TodosState> {
        public override string GetName() => "Todos";

        protected override TodosState GetInitialState() =>
            new TodosState(false, null, null, null);
    }


}
