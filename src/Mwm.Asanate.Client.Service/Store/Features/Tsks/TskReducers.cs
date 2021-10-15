using Fluxor;
using Mwm.Asanate.Client.Service.Store.Features.Shared.Actions;
using Mwm.Asanate.Client.Service.Store.State.Shared;
using Mwm.Asanate.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mwm.Asanate.Client.Service.Store.Features.Tsks {
    public static class TskReducers {
        [ReducerMethod]
        public static EntityState<Tsk> ReduceLoadTsksAction(EntityState<Tsk> state, LoadAction<Tsk> _) =>
            new EntityState<Tsk>(true, null, null, state.CurrentEntity);

        [ReducerMethod]
        public static EntityState<Tsk> ReduceLoadTsksSuccessAction(EntityState<Tsk> state, LoadSuccessAction<Tsk> action) =>
            new EntityState<Tsk>(false, null, action.Entities, state.CurrentEntity);

        [ReducerMethod]
        public static EntityState<Tsk> ReduceLoadTsksFailureAction(EntityState<Tsk> state, LoadFailureAction<Tsk> action) =>
            new EntityState<Tsk>(false, action.ErrorMessage, null, state.CurrentEntity);
    }
}


