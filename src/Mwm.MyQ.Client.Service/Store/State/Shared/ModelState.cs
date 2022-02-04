using Mwm.MyQ.Client.Service.Models;
using Mwm.MyQ.Client.Service.Store.Features.ModelFilters;
using Mwm.MyQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mwm.MyQ.Client.Service.Store.State.Shared;
public class ModelState<TModel, TEntity> : RootState where TModel : EntityModel<TEntity>
                                                     where TEntity : INamedEntity {

    public IEnumerable<TModel>? Models { get; }

    public IEnumerable<TModel>? FilteredModels { get; }

    public IEnumerable<ModelFilter<TModel, TEntity>> ModelFilters { get; }

    public TModel FindById(int id) => (_lookup != null) ? _lookup[id].SingleOrDefault() : default;

    public TModel? CurrentModel { get; }

    private ILookup<int, TModel> _lookup;

    public ModelState() : this(false, null, null, null, ModelFilterTypes.DefaultModelFilters<TModel, TEntity>(), default) {
    }

    public ModelState(bool isLoading = false, 
                      string? currentErrorMessage = null, 
                      IEnumerable<TModel>? models = null, 
                      IEnumerable<TModel>? filteredModels = null,
                      IEnumerable<ModelFilter<TModel, TEntity>> modelFilters = null,
                      TModel? currentModel = default) :
        base(isLoading, currentErrorMessage) {
        _lookup = models?.ToLookup(e => e.Id);
        Models = models;
        FilteredModels = filteredModels;
        ModelFilters = modelFilters;
        CurrentModel = currentModel;
    }
}