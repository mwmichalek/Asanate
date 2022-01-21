using Mwm.MyQ.Client.Service.Models;
using Mwm.MyQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mwm.MyQ.Client.Service.Store.Features.Shared.Actions {

    public class LoadModelSuccessAction<TEntity> where TEntity : INamedEntity {

        public LoadModelSuccessAction(IEnumerable<EntityModel<TEntity>> models, IEnumerable<EntityModel<TEntity>> filteredModels) => 
            (Models, FilteredModels) = (models, filteredModels);

        public IEnumerable<EntityModel<TEntity>> Models { get; }

        public IEnumerable<EntityModel<TEntity>> FilteredModels { get; }

    }

    public class LoadModelFailureAction<TEntity> : FailureAction where TEntity : INamedEntity {

        public LoadModelFailureAction(string errorMessage) : base(errorMessage) { }
    }

}
