using Mwm.MyQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mwm.MyQ.Client.Service.Store.Features.Shared.Actions {
    public class LoadEntityAction<TEntity> where TEntity : INamedEntity {
    }

    public class LoadEntitySuccessAction<TEntity> where TEntity : INamedEntity {

        public LoadEntitySuccessAction(IEnumerable<TEntity> entities) =>
            Entities = entities;

        public IEnumerable<TEntity> Entities { get; }

    }

    public class LoadEntityFailureAction<TEntity> : FailureAction where TEntity : INamedEntity {

        public LoadEntityFailureAction(string errorMessage) : base(errorMessage) { }
    }

}
