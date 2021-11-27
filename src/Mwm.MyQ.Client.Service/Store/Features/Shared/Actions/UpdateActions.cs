using Mwm.MyQ.Domain;
using Mwm.MyQ.Application.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mwm.MyQ.Client.Service.Store.Features.Shared.Actions {
    public class UpdateAction<TEntity, TUpdateEntityAction> where TEntity : INamedEntity
                                                           where TUpdateEntityAction : IUpdateEntityCommand<TEntity> {

        public TUpdateEntityAction EntityCommand {  get; }

        public UpdateAction(TUpdateEntityAction entityCommand) => EntityCommand = entityCommand;
    }

    public class UpdateSuccessAction<TEntity> where TEntity : INamedEntity {

        public UpdateSuccessAction(TEntity entity) => Entity = entity;

        public TEntity Entity { get; }

    }

    public class UpdateFailureAction<TEntity> : FailureAction where TEntity : INamedEntity {

        public UpdateFailureAction(string errorMessage) : base(errorMessage) { }
    }

}
