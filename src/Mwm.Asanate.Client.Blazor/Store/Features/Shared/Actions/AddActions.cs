using Mwm.Asanate.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mwm.Asanate.Client.Blazor.Store.Features.Shared.Actions {
    public class AddAction<TEntity> where TEntity : INamedEntity {

        public AddAction(TEntity entity) =>
            Entity = entity;

        public TEntity Entity { get; }
    }

    public class AddSuccessAction<TEntity> where TEntity : INamedEntity {

        public AddSuccessAction(int id) =>
            Id = id;

        public int Id { get; }

    }

    public class AddFailureAction<TEntity> : FailureAction where TEntity : INamedEntity {

        public AddFailureAction(string errorMessage) : base(errorMessage) { }
    }

}
