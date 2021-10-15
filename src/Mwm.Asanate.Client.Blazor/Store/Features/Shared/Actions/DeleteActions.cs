using Mwm.Asanate.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mwm.Asanate.Client.Blazor.Store.Features.Shared.Actions {
    public class DeleteAction<TEntity> where TEntity : INamedEntity {
    }

    public class DeleteSuccessAction<TEntity> where TEntity : INamedEntity {

        public DeleteSuccessAction(TEntity entity) =>
            Entity = entity;

        public TEntity Entity { get; }

    }

    public class DeleteFailureAction<TEntity> : FailureAction where TEntity : INamedEntity {

        public DeleteFailureAction(string errorMessage) : base(errorMessage) { }
    }

}
