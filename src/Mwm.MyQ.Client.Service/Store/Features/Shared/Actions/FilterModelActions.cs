using Mwm.MyQ.Client.Service.Models;
using Mwm.MyQ.Client.Service.Store.Features.ModelFilters;
using Mwm.MyQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mwm.MyQ.Client.Service.Store.Features.Shared.Actions {

    public class FilterModelAction<TModel, TEntity> where TModel : EntityModel<TEntity>
                                                    where TEntity : INamedEntity {

        public FilterModelAction(ModelFilter<TModel, TEntity> modelFilter) =>
            (ModelFilter) = (modelFilter);

        public ModelFilter<TModel, TEntity> ModelFilter { get; }

    }

    public class FilterModelSuccessAction<TModel, TEntity> where TModel : EntityModel<TEntity> 
                                                           where TEntity : INamedEntity {

        public FilterModelSuccessAction(ModelFilter<TModel, TEntity> currentModelFilter,
                                        IEnumerable<ModelFilter<TModel, TEntity>> modelFilters,
                                        IEnumerable<TModel> filteredModels) =>
            (CurrentModelFilter, ModelFilters, FilteredModels) = (currentModelFilter, modelFilters, filteredModels);

        public ModelFilter<TModel, TEntity> CurrentModelFilter { get; }

        public IEnumerable<ModelFilter<TModel, TEntity>> ModelFilters { get; }

        public IEnumerable<TModel> FilteredModels { get; }

    }

    public class FilterModelFailureAction<TModel, TEntity> : FailureAction where TModel : EntityModel<TEntity> 
                                                                         where TEntity : INamedEntity {
        public FilterModelFailureAction(string errorMessage) : base(errorMessage) { }
    }

}
