using Fluxor;
using Mwm.MyQ.Client.Service.Store.State.Shared;
using Mwm.MyQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.MyQ.Client.Service.Store.Features.Shared;

public abstract class ModelFeature<TEntity> : Feature<ModelState<TEntity>> where TEntity : INamedEntity {
    public override string GetName() => typeof(TEntity).Name;

    protected override ModelState<TEntity> GetInitialState() => Activator.CreateInstance<ModelState<TEntity>>();

}
