using Fluxor;
using Mwm.MyQ.Client.Service.Store.Features.Shared.Actions;
using Mwm.MyQ.Client.Service.Store.State.Shared;
using Mwm.MyQ.Application.Initiatives.Commands;
using Mwm.MyQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mwm.MyQ.Client.Service.Store.Features.Initiatives {
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




        [ReducerMethod]
        public static EntityState<Initiative> ReduceAddInitiativesAction(EntityState<Initiative> state, AddAction<Initiative, InitiativeAdd.Command> _) =>
           new EntityState<Initiative>(true, null, state.Entities, state.CurrentEntity);

        [ReducerMethod]
        public static EntityState<Initiative> ReduceAddInitiativesSuccessAction(EntityState<Initiative> state, AddSuccessAction<Initiative> action) {
            var entities = state.Entities.ToList();
            entities.Add(action.Entity);
            return new EntityState<Initiative>(false, null, entities, action.Entity);
        }

        [ReducerMethod]
        public static EntityState<Initiative> ReduceAddInitiativesFailureAction(EntityState<Initiative> state, AddFailureAction<Initiative> action) =>
            new EntityState<Initiative>(false, action.ErrorMessage, state.Entities, state.CurrentEntity);




        [ReducerMethod]
        public static EntityState<Initiative> ReduceUpdateInitiativesAction(EntityState<Initiative> state, UpdateAction<Initiative, InitiativeUpdate.Command> _) =>
           new EntityState<Initiative>(true, null, state.Entities, state.CurrentEntity);

        [ReducerMethod]
        public static EntityState<Initiative> ReduceUpdateInitiativesSuccessAction(EntityState<Initiative> state, UpdateSuccessAction<Initiative> action) {
            var entities = state.Entities.ToList();
            var oldEntity = entities.SingleOrDefault(e => e.Id == action.Entity.Id);
            if (oldEntity != null)
                entities.Remove(oldEntity);
            entities.Add(action.Entity);
            return new EntityState<Initiative>(false, null, entities, action.Entity);
        }

        [ReducerMethod]
        public static EntityState<Initiative> ReduceUpdateInitiativesFailureAction(EntityState<Initiative> state, UpdateFailureAction<Initiative> action) =>
            new EntityState<Initiative>(false, action.ErrorMessage, state.Entities, state.CurrentEntity);




        [ReducerMethod]
        public static EntityState<Initiative> ReduceDeleteInitiativesAction(EntityState<Initiative> state, DeleteAction<Initiative, InitiativeDelete.Command> _) =>
           new EntityState<Initiative>(true, null, state.Entities, state.CurrentEntity);

        [ReducerMethod]
        public static EntityState<Initiative> ReduceDeleteInitiativesSuccessAction(EntityState<Initiative> state, DeleteSuccessAction<Initiative> action) {
            var entities = state.Entities.ToList();
            var oldEntity = entities.SingleOrDefault(e => e.Id == action.Id);
            if (oldEntity != null)
                entities.Remove(oldEntity);

            return new EntityState<Initiative>(false, null, entities, null);
        }

        [ReducerMethod]
        public static EntityState<Initiative> ReduceDeleteInitiativesFailureAction(EntityState<Initiative> state, DeleteFailureAction<Initiative> action) =>
            new EntityState<Initiative>(false, action.ErrorMessage, state.Entities, state.CurrentEntity);
    }
}


