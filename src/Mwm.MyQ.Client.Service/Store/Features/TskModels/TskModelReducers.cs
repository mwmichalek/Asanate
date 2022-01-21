using Fluxor;
using Mwm.MyQ.Client.Service.Store.Features.Shared.Actions;
using Mwm.MyQ.Client.Service.Store.State.Shared;
using Mwm.MyQ.Domain;
using Mwm.MyQ.Application.Tsks.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mwm.MyQ.Client.Service.Models;

namespace Mwm.MyQ.Client.Service.Store.Features.Tsks {
    public static class TskModelReducers {
        //LoadModelSuccessAction<TModelEntity>
        [ReducerMethod]
        public static ModelState<TskModel, Tsk> ReduceLoadModelSuccessAction(ModelState<TskModel, Tsk> state, LoadModelSuccessAction<TskModel, Tsk> action) =>
            new ModelState<TskModel, Tsk>(false, null, action.Models, action.FilteredModels, null);

        //[ReducerMethod]
        //public static ModelState<Tsk> ReduceLoadTsksFailureAction(ModelState<Tsk> state, LoadEntityFailureAction<Tsk> action) =>
        //    new ModelState<Tsk>(false, action.ErrorMessage, null, state.CurrentModel);



    }
}


