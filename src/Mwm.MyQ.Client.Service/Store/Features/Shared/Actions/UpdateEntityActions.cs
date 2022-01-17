using Mwm.MyQ.Domain;
using Mwm.MyQ.Application.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mwm.MyQ.Client.Service.Store.Features.Shared.Actions {
    public class UpdateEntityAction<TEntity, TUpdateEntityAction> where TEntity : INamedEntity
                                                           where TUpdateEntityAction : IUpdateEntityCommand<TEntity> {

        public TUpdateEntityAction EntityCommand {  get; }

        public UpdateEntityAction(TUpdateEntityAction entityCommand) => EntityCommand = entityCommand;
    }

    public class UpdateEntitySuccessAction<TEntity> where TEntity : INamedEntity {

        public UpdateEntitySuccessAction(TEntity entity) => Entity = entity;

        public TEntity Entity { get; }

    }

    public class UpdateEntityFailureAction<TEntity> : FailureAction where TEntity : INamedEntity {

        public UpdateEntityFailureAction(string errorMessage) : base(errorMessage) { }
    }

}
