using Fluxor;
using Microsoft.Extensions.Logging;
using Mwm.MyQ.Application.Shared.Commands;
using Mwm.MyQ.Client.Service.Store.Features.Shared.Actions;
using Mwm.MyQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mwm.MyQ.Client.Service.Facades {
    public class EntityStateFacade {

        private readonly ILogger<EntityStateFacade> _logger;
        private readonly IDispatcher _dispatcher;

        public EntityStateFacade(ILogger<EntityStateFacade> logger, IDispatcher dispatcher) =>
            (_logger, _dispatcher) = (logger, dispatcher);

        public void Load<TEntity>() where TEntity : INamedEntity {
            _logger.LogInformation($"Issuing action to load { typeof(TEntity).Name}(s) ...");
            _dispatcher.Dispatch(new LoadAction<TEntity>());
        }

        public void Add<TEntity, TAddEntityCommand>(TAddEntityCommand entityCommand) where TEntity : INamedEntity
                                                                                     where TAddEntityCommand : IAddEntityCommand<TEntity> {
            _logger.LogInformation($"Issuing action to add { typeof(TEntity).Name}(s) ...");
            _dispatcher.Dispatch(new AddAction<TEntity, TAddEntityCommand>(entityCommand));
        }

        public void Update<TEntity, TUpdateEntityCommand>(TUpdateEntityCommand entityCommand) where TEntity : INamedEntity
                                                                                              where TUpdateEntityCommand : IUpdateEntityCommand<TEntity>{
            _logger.LogInformation($"Issuing action to update { typeof(TEntity).Name}(s) ...");
            _dispatcher.Dispatch(new UpdateAction<TEntity, TUpdateEntityCommand>(entityCommand));
        }

        public void Delete<TEntity, TDeleteEntityCommand>(TDeleteEntityCommand entityCommand) where TEntity : INamedEntity
                                                                                              where TDeleteEntityCommand : IDeleteEntityCommand<TEntity> {
            _logger.LogInformation($"Issuing action to delete { typeof(TEntity).Name}(s) ...");
            _dispatcher.Dispatch(new DeleteAction<TEntity, TDeleteEntityCommand>(entityCommand));
        }
    }
}
