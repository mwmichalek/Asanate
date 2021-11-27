using Mwm.MyQ.Application.Shared.Commands;
using Mwm.MyQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mwm.MyQ.Client.Service.Store.Features.Shared.Actions {

    public class DeleteAction<TEntity, TDeleteEntityCommand> where TEntity : INamedEntity
                                                             where TDeleteEntityCommand : IDeleteEntityCommand<TEntity> {

        public DeleteAction(TDeleteEntityCommand entityCommand) =>
            EntityCommand = entityCommand;

        public TDeleteEntityCommand EntityCommand { get; }
    }

    public class DeleteSuccessAction<TEntity> where TEntity : INamedEntity {

        public DeleteSuccessAction(int id) =>
            Id = id;

        public int Id { get; }

    }

    public class DeleteFailureAction<TEntity> : FailureAction where TEntity : INamedEntity {

        public DeleteFailureAction(string errorMessage) : base(errorMessage) { }
    }

}
