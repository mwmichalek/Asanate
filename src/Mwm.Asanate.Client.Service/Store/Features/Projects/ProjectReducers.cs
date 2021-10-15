using Fluxor;
using Mwm.Asanate.Client.Service.Store.Features.Shared.Actions;
using Mwm.Asanate.Client.Service.Store.State.Shared;
using Mwm.Asanate.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mwm.Asanate.Client.Service.Store.Features.Tsks {
    public static class ProjectReducers {
        [ReducerMethod]
        public static EntityState<Project> ReduceLoadProjectsAction(EntityState<Project> state, LoadAction<Project> _) =>
            new EntityState<Project>(true, null, null, state.CurrentEntity);

        [ReducerMethod]
        public static EntityState<Project> ReduceLoadProjectsSuccessAction(EntityState<Project> state, LoadSuccessAction<Project> action) =>
            new EntityState<Project>(false, null, action.Entities, state.CurrentEntity);

        [ReducerMethod]
        public static EntityState<Project> ReduceLoadProjectsFailureAction(EntityState<Project> state, LoadFailureAction<Project> action) =>
            new EntityState<Project>(false, action.ErrorMessage, null, state.CurrentEntity);
    }
}


