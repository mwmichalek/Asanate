using Fluxor;
using Mwm.Asanate.Client.Service.Store.State.Shared;
using Mwm.Asanate.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mwm.Asanate.Client.Service.Store.Features.Shared {
    public abstract class EntityFeature<TEntity> : Feature<EntityState<TEntity>> where TEntity : INamedEntity {
        public override string GetName() => typeof(TEntity).Name;

        protected override EntityState<TEntity> GetInitialState() => Activator.CreateInstance<EntityState<TEntity>>();

    }

}
