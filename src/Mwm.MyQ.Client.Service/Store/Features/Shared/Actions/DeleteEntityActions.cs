using Mwm.MyQ.Application.Shared.Commands;
using Mwm.MyQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mwm.MyQ.Client.Service.Store.Features.Shared.Actions {

    public class DeleteEntityAction<TEntity, TDeleteEntityCommand> where TEntity : INamedEntity
                                                             where TDeleteEntityCommand : IDeleteEntityCommand<TEntity> {

        public DeleteEntityAction(TDeleteEntityCommand entityCommand) =>
            EntityCommand = entityCommand;

        public TDeleteEntityCommand EntityCommand { get; }
    }

    public class DeleteEntitySuccessAction<TEntity> where TEntity : INamedEntity {

        public DeleteEntitySuccessAction(int id) =>
            Id = id;

        public int Id { get; }

    }

    public class DeleteEntityFailureAction<TEntity> : FailureAction where TEntity : INamedEntity {

        public DeleteEntityFailureAction(string errorMessage) : base(errorMessage) { }
    }

}
