using Mwm.Asanate.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mwm.Asanate.Client.Service.Store.Features.Shared.Actions {
    public class LoadAction<TEntity> where TEntity : INamedEntity {
    }

    public class LoadSuccessAction<TEntity> where TEntity : INamedEntity {

        public LoadSuccessAction(IEnumerable<TEntity> entities) =>
            Entities = entities;

        public IEnumerable<TEntity> Entities { get; }

    }

    public class LoadFailureAction<TEntity> : FailureAction where TEntity : INamedEntity {

        public LoadFailureAction(string errorMessage) : base(errorMessage) { }
    }

}
