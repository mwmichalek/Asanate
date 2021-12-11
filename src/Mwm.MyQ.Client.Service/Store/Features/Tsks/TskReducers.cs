using Fluxor;
using Mwm.MyQ.Client.Service.Store.Features.Shared.Actions;
using Mwm.MyQ.Client.Service.Store.State.Shared;
using Mwm.MyQ.Domain;
using Mwm.MyQ.Application.Tsks.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mwm.MyQ.Client.Service.Store.Features.Tsks {
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




        [ReducerMethod]
        public static EntityState<Tsk> ReduceAddTsksAction(EntityState<Tsk> state, AddAction<Tsk, TskAdd.Command> _) =>
           new EntityState<Tsk>(true, null, state.Entities, state.CurrentEntity);

        [ReducerMethod]
        public static EntityState<Tsk> ReduceAddTsksSuccessAction(EntityState<Tsk> state, AddSuccessAction<Tsk> action) {
            var entities = state.Entities.ToList();
            entities.Add(action.Entity);
            return new EntityState<Tsk>(false, null, entities, action.Entity);
        }

        [ReducerMethod]
        public static EntityState<Tsk> ReduceAddTsksFailureAction(EntityState<Tsk> state, AddFailureAction<Tsk> action) =>
            new EntityState<Tsk>(false, action.ErrorMessage, state.Entities, state.CurrentEntity);




        [ReducerMethod]
        public static EntityState<Tsk> ReduceUpdateTsksAction(EntityState<Tsk> state, UpdateAction<Tsk, TskUpdate.Command> _) =>
           new EntityState<Tsk>(true, null, state.Entities, state.CurrentEntity);

        [ReducerMethod]
        public static EntityState<Tsk> ReduceUpdateTsksSuccessAction(EntityState<Tsk> state, UpdateSuccessAction<Tsk> action) {
            var entities = state.Entities.ToList();
            var oldEntity = entities.SingleOrDefault(e => e.Id == action.Entity.Id);
            if (oldEntity != null)
                entities.Remove(oldEntity);
            entities.Add(action.Entity);
            return new EntityState<Tsk>(false, null, entities, action.Entity);
        }

        [ReducerMethod]
        public static EntityState<Tsk> ReduceUpdateTsksFailureAction(EntityState<Tsk> state, UpdateFailureAction<Tsk> action) =>
            new EntityState<Tsk>(false, action.ErrorMessage, state.Entities, state.CurrentEntity);




        [ReducerMethod]
        public static EntityState<Tsk> ReduceDeleteTsksAction(EntityState<Tsk> state, DeleteAction<Tsk, TskDelete.Command> _) =>
           new EntityState<Tsk>(true, null, state.Entities, state.CurrentEntity);

        [ReducerMethod]
        public static EntityState<Tsk> ReduceDeleteTsksSuccessAction(EntityState<Tsk> state, DeleteSuccessAction<Tsk> action) {
            var entities = state.Entities.ToList();
            var oldEntity = entities.SingleOrDefault(e => e.Id == action.Id);
            if (oldEntity != null)
                entities.Remove(oldEntity);

            return new EntityState<Tsk>(false, null, entities, null);
        }

        [ReducerMethod]
        public static EntityState<Tsk> ReduceDeleteTsksFailureAction(EntityState<Tsk> state, DeleteFailureAction<Tsk> action) =>
            new EntityState<Tsk>(false, action.ErrorMessage, state.Entities, state.CurrentEntity);
    }
}


