using Mwm.MyQ.Client.Service.Models;
using Mwm.MyQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.MyQ.Client.Service.Store.State.Shared;

public class TskModelState : RootState {

    public IEnumerable<TskModel>? Models { get; }

    public TskModel FindById(int id) => (_lookup != null) ? _lookup[id].SingleOrDefault() : default;

    public TskModel? CurrentModel { get; }

    private ILookup<int, TskModel> _lookup;

    public TskModelState() : this(false, null, null, default) {
    }

    public TskModelState(bool isLoading = false, string? currentErrorMessage = null, IEnumerable<TskModel>? models = null, TskModel? currentModel = default) :
        base(isLoading, currentErrorMessage) {
        _lookup = models?.ToLookup(e => e.Id);
        Models = models;
        CurrentModel = CurrentModel;
    }
}



