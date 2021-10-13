using Mwm.Asanate.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mwm.Asanate.Client.Blazor.Store.Features.Shared.Actions {
    public class UpdateAction<TEntity> where TEntity : INamedEntity {

        public TEntity Entity {  get; }

        public UpdateAction(TEntity entity) => Entity = entity;
    }

    public class UpdateSuccessAction<TEntity> where TEntity : INamedEntity {

        public UpdateSuccessAction(TEntity entity) => Entity = entity;

        public TEntity Entity { get; }

    }

    public class UpdateFailureAction<TEntity> : FailureAction where TEntity : INamedEntity {

        public UpdateFailureAction(string errorMessage) : base(errorMessage) { }
    }

}
