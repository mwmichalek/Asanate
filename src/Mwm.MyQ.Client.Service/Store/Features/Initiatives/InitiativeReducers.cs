using Fluxor;
using Mwm.MyQ.Client.Service.Store.Features.Shared.Actions;
using Mwm.MyQ.Client.Service.Store.State.Shared;
using Mwm.MyQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mwm.MyQ.Client.Service.Store.Features.Tsks {
    public static class InitiativeReducers {
        [ReducerMethod]
        public static EntityState<Initiative> ReduceLoadInitiativesAction(EntityState<Initiative> state, LoadAction<Initiative> _) =>
            new EntityState<Initiative>(true, null, null, state.CurrentEntity);

        [ReducerMethod]
        public static EntityState<Initiative> ReduceLoadInitiativesSuccessAction(EntityState<Initiative> state, LoadSuccessAction<Initiative> action) =>
            new EntityState<Initiative>(false, null, action.Entities, state.CurrentEntity);

        [ReducerMethod]
        public static EntityState<Initiative> ReduceLoadInitiativesFailureAction(EntityState<Initiative> state, LoadFailureAction<Initiative> action) =>
            new EntityState<Initiative>(false, action.ErrorMessage, null, state.CurrentEntity);
    }
}


