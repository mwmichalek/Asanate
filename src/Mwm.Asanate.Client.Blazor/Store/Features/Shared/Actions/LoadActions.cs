using Mwm.Asanate.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mwm.Asanate.Client.Blazor.Store.Features.Shared.Actions {
    public class LoadAction<TEntity> where TEntity : NamedEntity {
    }

    public class LoadSuccessAction<TEntity> where TEntity : NamedEntity {

    }

    public class LoadFailureAction<TEntity> where TEntity : NamedEntity {

    }
}
