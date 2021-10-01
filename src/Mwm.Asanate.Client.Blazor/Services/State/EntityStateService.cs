using Fluxor;
using Microsoft.Extensions.Logging;
using Mwm.Asanate.Client.Blazor.Store.Features.Shared.Actions;
using Mwm.Asanate.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mwm.Asanate.Client.Blazor.Services.State {
    public class EntityStateService {

        private readonly ILogger<EntityStateService> _logger;
        private readonly IDispatcher _dispatcher;

        public EntityStateService(ILogger<EntityStateService> logger, IDispatcher dispatcher) =>
            (_logger, _dispatcher) = (logger, dispatcher);

        public void Load<TEntity>() where TEntity : INamedEntity {
            _logger.LogInformation($"Issuing action to load { typeof(TEntity).Name}(s) ...");
            _dispatcher.Dispatch(new LoadAction<TEntity>());
        }
    }
}
