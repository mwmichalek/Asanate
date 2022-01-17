using Mwm.MyQ.Client.Service.Models;
using Mwm.MyQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mwm.MyQ.Client.Service.Store.State.Shared;
public abstract class ModelState<TModel> : RootState where TModel : EntityModel<TNamedEntity> where TNamedEntity : INamedEntity {

    public IEnumerable<EntityModel<TEntity>>? Models { get; }

    public EntityModel<TEntity> FindById(int id) => (_lookup != null) ? _lookup[id].SingleOrDefault() : default;

    public EntityModel<TEntity>? CurrentModel { get; }

    private ILookup<int, TskModel> _lookup;

    public ModelState() : this(false, null, null, default) {
    }

    public ModelState(bool isLoading = false, string? currentErrorMessage = null, IEnumerable<EntityModel<TEntity>>? models = null, TskModel? currentModel = default) :
        base(isLoading, currentErrorMessage) {
        _lookup = models?.ToLookup(e => e.Id);
        Models = models;
        CurrentModel = CurrentModel;
    }
}