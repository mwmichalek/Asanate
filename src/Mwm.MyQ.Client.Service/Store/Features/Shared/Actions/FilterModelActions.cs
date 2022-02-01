using Mwm.MyQ.Client.Service.Models;
using Mwm.MyQ.Client.Service.Store.Features.ModelFilters;
using Mwm.MyQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mwm.MyQ.Client.Service.Store.Features.Shared.Actions {

    public class FilterModelAction<TModelFilter, TModel, TEntity> where TModelFilter : ModelFilter<TModel, TEntity>
                                                                  where TModel : EntityModel<TEntity>
                                                                  where TEntity : INamedEntity {

        public FilterModelAction(TModelFilter modelFilter) =>
            (ModelFilter) = (modelFilter);

        public TModelFilter ModelFilter { get; }

    }

    public class FilterModelSuccessAction<TModelFilter, TModel, TEntity> where TModelFilter : ModelFilter<TModel, TEntity> 
                                                                         where TModel : EntityModel<TEntity> 
                                                                         where TEntity : INamedEntity {

        public FilterModelSuccessAction(TModelFilter modelFilter) =>
            (ModelFilter) = (modelFilter);

        public TModelFilter ModelFilter { get; }

    }

    public class FilterModelFailureAction<TModel, TEntity> : FailureAction where TModel : EntityModel<TEntity> 
                                                                         where TEntity : INamedEntity {
        public FilterModelFailureAction(string errorMessage) : base(errorMessage) { }
    }

}
