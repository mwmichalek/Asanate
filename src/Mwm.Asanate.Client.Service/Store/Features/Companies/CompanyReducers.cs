using Fluxor;
using Mwm.Asanate.Client.Service.Store.Features.Shared.Actions;
using Mwm.Asanate.Client.Service.Store.State.Shared;
using Mwm.Asanate.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mwm.Asanate.Client.Service.Store.Features.Tsks {
    public static class CompanyReducers {
        [ReducerMethod]
        public static EntityState<Company> ReduceLoadCompaniesAction(EntityState<Company> state, LoadAction<Company> _) =>
            new EntityState<Company>(true, null, null, state.CurrentEntity);

        [ReducerMethod]
        public static EntityState<Company> ReduceLoadCompaniesSuccessAction(EntityState<Company> state, LoadSuccessAction<Company> action) =>
            new EntityState<Company>(false, null, action.Entities, state.CurrentEntity);

        [ReducerMethod]
        public static EntityState<Company> ReduceLoadCompaniesFailureAction(EntityState<Company> state, LoadFailureAction<Company> action) =>
            new EntityState<Company>(false, action.ErrorMessage, null, state.CurrentEntity);
    }
}


