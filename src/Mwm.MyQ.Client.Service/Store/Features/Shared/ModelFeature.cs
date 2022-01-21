using Fluxor;
using Mwm.MyQ.Client.Service.Models;
using Mwm.MyQ.Client.Service.Store.State.Shared;
using Mwm.MyQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.MyQ.Client.Service.Store.Features.Shared;

public abstract class ModelFeature<TModel, TEntity> : Feature<ModelState<TModel, TEntity>> where TModel : EntityModel<TEntity>
                                                                                           where TEntity : INamedEntity {
    public override string GetName() => typeof(TModel).Name;

    protected override ModelState<TModel, TEntity> GetInitialState() => Activator.CreateInstance<ModelState<TModel, TEntity>>();

}
