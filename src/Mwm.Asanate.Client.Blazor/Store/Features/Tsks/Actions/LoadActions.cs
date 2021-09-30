using Mwm.Asanate.Client.Blazor.Store.Features.Shared.Actions;
using Mwm.Asanate.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mwm.Asanate.Client.Blazor.Store.Features.Tsks.Actions {
    public class LoadTsksAction {
    }

    public class LoadTsksFailureAction : FailureAction {

        public LoadTsksFailureAction(string errorMessage)
            : base(errorMessage) {
        }

    }

    public class LoadTsksSuccessAction {
        public LoadTsksSuccessAction(IEnumerable<Tsk> tsks) =>
            Tsks = tsks;

        public IEnumerable<Tsk> Tsks { get; }
    }
}
