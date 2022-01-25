using Mwm.MyQ.Client.Service.Models;
using Mwm.MyQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mwm.MyQ.Client.Service.Store.Features.Shared.Actions {

    public class LoadModelAction<TModel, TEntity> where TModel : EntityModel<TEntity>
                                                  where TEntity : INamedEntity {

        public LoadModelAction(IEnumerable<TEntity> entities) =>
            (Entities) = (entities);

        public IEnumerable<TEntity> Entities { get; }

    }

    public class LoadModelSuccessAction<TModel, TEntity> where TModel : EntityModel<TEntity> 
                                                         where TEntity : INamedEntity {

        public LoadModelSuccessAction(IEnumerable<TModel> models, IEnumerable<TModel> filteredModels) => 
            (Models, FilteredModels) = (models, filteredModels);

        public IEnumerable<TModel> Models { get; }

        public IEnumerable<TModel> FilteredModels { get; }

    }

    public class LoadModelFailureAction<TModel, TEntity> : FailureAction where TModel : EntityModel<TEntity> 
                                                                         where TEntity : INamedEntity {

        public LoadModelFailureAction(string errorMessage) : base(errorMessage) { }
    }

}
