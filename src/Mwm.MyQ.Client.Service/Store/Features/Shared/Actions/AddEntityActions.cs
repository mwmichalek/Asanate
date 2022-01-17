using Mwm.MyQ.Application.Shared.Commands;
using Mwm.MyQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mwm.MyQ.Client.Service.Store.Features.Shared.Actions {
    public class AddEntityAction<TEntity, TAddEntityCommand> where TEntity : INamedEntity
                                                       where TAddEntityCommand : IAddEntityCommand<TEntity> {

        public AddEntityAction(TAddEntityCommand entityCommand) =>
            EntityCommand = entityCommand;

        public TAddEntityCommand EntityCommand { get; }
    }

    public class AddEntitySuccessAction<TEntity> where TEntity : INamedEntity {

        public AddEntitySuccessAction(TEntity entity) => Entity = entity;

        public TEntity Entity { get; }

    }

    public class AddEntityFailureAction<TEntity> : FailureAction where TEntity : INamedEntity {

        public AddEntityFailureAction(string errorMessage) : base(errorMessage) { }
    }

}
