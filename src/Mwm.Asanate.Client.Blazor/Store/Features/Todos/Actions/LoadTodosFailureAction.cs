using Mwm.Asanate.Client.Blazor.Store.Features.Shared.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mwm.Asanate.Client.Blazor.Store.Features.Todos.Actions {
    public class LoadTodosFailureAction : FailureAction {
        public LoadTodosFailureAction(string errorMessage)
            : base(errorMessage) {
        }
    }
}
