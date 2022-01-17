using Mwm.MyQ.Client.Service.Models;
using Mwm.MyQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mwm.MyQ.Client.Service.Store.Features.Shared.Actions {
    public class LoadModelAction<TEntity> where TEntity : INamedEntity {
    }

    public class LoadModelSuccessAction<TEntity> where TEntity : INamedEntity {

        public LoadModelSuccessAction(IEnumerable<EntityModel<TEntity>> models) =>
            Models = models;

        public IEnumerable<EntityModel<TEntity>> Models { get; }

    }

    public class LoadModelFailureAction<TEntity> : FailureAction where TEntity : INamedEntity {

        public LoadModelFailureAction(string errorMessage) : base(errorMessage) { }
    }

}
