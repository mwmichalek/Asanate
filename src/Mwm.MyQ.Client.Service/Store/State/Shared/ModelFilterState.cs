using Mwm.MyQ.Client.Service.Models;
using Mwm.MyQ.Client.Service.Store.Features.ModelFilters;
using Mwm.MyQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mwm.MyQ.Client.Service.Store.State.Shared;
public class ModelFilterState<TModelFilter, TModel, TEntity> : RootState where TModelFilter : ModelFilter<TModel, TEntity>
                                                                         where TModel : EntityModel<TEntity>
                                                                         where TEntity : INamedEntity {

    public IEnumerable<TModelFilter>? ModelFilters { get; }

 
    public TModelFilter? CurrentModelFilter { get; }

    public ModelFilterState() : this(false, null, null, default) {
    }

    public ModelFilterState(bool isLoading = false, string? currentErrorMessage = null, IEnumerable<TModelFilter>? modelFilters = null, TModelFilter? currentModelFilter = default) :
        base(isLoading, currentErrorMessage) {
        ModelFilters = modelFilters;
        CurrentModelFilter = currentModelFilter;
    }
}