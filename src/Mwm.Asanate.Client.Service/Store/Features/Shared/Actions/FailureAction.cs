using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mwm.Asanate.Client.Service.Store.Features.Shared.Actions {
    public abstract class FailureAction {
        protected FailureAction(string errorMessage) =>
            ErrorMessage = errorMessage;

        public string ErrorMessage { get; }
    }

}
