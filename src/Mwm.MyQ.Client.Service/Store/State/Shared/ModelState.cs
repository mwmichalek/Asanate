using Mwm.MyQ.Client.Service.Models;
using Mwm.MyQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mwm.MyQ.Client.Service.Store.State.Shared;
public abstract class ModelState<TNamedEntity> : RootState where TNamedEntity : INamedEntity {

    public IEnumerable<EntityModel<TNamedEntity>>? Models { get; }

    public EntityModel<TNamedEntity> FindById(int id) => (_lookup != null) ? _lookup[id].SingleOrDefault() : default;

    public EntityModel<TNamedEntity>? CurrentModel { get; }

    private ILookup<int, EntityModel<TNamedEntity>> _lookup;

    public ModelState() : this(false, null, null, default) {
    }

    public ModelState(bool isLoading = false, string? currentErrorMessage = null, IEnumerable<EntityModel<TNamedEntity>>? models = null, EntityModel<TNamedEntity>? currentModel = default) :
        base(isLoading, currentErrorMessage) {
        _lookup = models?.ToLookup(e => e.Id);
        Models = models;
        CurrentModel = CurrentModel;
    }
}