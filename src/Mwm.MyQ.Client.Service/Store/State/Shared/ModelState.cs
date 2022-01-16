using Mwm.MyQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mwm.MyQ.Client.Service.Store.State.Shared {
    public class ModelState<TEntity> : RootState where TEntity : INamedEntity {

        public IEnumerable<TEntity>? Entities { get; }

        public TEntity FindById(int id) => (_lookup != null) ? _lookup[id].SingleOrDefault() : default;

        public TEntity? CurrentEntity { get; }

        private ILookup<int, TEntity> _lookup;

        public ModelState() : this(false, null, null, default) {
        }

        public ModelState(bool isLoading = false, string? currentErrorMessage = null, IEnumerable<TEntity>? entities = null, TEntity? currentEntity = default) : 
            base(isLoading, currentErrorMessage) {
            _lookup = entities?.ToLookup(e => e.Id);
            Entities = entities;
            CurrentEntity = currentEntity;
        }

    }

}
