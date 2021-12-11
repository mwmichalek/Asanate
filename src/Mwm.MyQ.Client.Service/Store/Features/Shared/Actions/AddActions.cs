using Mwm.MyQ.Application.Shared.Commands;
using Mwm.MyQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mwm.MyQ.Client.Service.Store.Features.Shared.Actions {
    public class AddAction<TEntity, TAddEntityCommand> where TEntity : INamedEntity
                                                       where TAddEntityCommand : IAddEntityCommand<TEntity> {

        public AddAction(TAddEntityCommand entityCommand) =>
            EntityCommand = entityCommand;

        public TAddEntityCommand EntityCommand { get; }
    }

    public class AddSuccessAction<TEntity> where TEntity : INamedEntity {

        public AddSuccessAction(TEntity entity) => Entity = entity;

        public TEntity Entity { get; }

    }

    public class AddFailureAction<TEntity> : FailureAction where TEntity : INamedEntity {

        public AddFailureAction(string errorMessage) : base(errorMessage) { }
    }

}
