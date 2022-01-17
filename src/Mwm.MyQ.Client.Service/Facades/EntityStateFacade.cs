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

        public async Task Load<TEntity>() where TEntity : INamedEntity {
            _logger.LogInformation($"Issuing action to load { typeof(TEntity).Name}(s) ...");
            await Task.Run(() => _dispatcher.Dispatch(new LoadEntityAction<TEntity>()));
        }

        public async Task Add<TEntity, TAddEntityCommand>(TAddEntityCommand entityCommand) where TEntity : INamedEntity
                                                                                     where TAddEntityCommand : IAddEntityCommand<TEntity> {
            _logger.LogInformation($"Issuing action to add { typeof(TEntity).Name}(s) ...");
            await Task.Run(() => _dispatcher.Dispatch(new AddEntityAction<TEntity, TAddEntityCommand>(entityCommand)));
        }

        public async Task Update<TEntity, TUpdateEntityCommand>(TUpdateEntityCommand entityCommand) where TEntity : INamedEntity
                                                                                              where TUpdateEntityCommand : IUpdateEntityCommand<TEntity>{
            _logger.LogInformation($"Issuing action to update { typeof(TEntity).Name}(s) ...");
            await Task.Run(() => _dispatcher.Dispatch(new UpdateEntityAction<TEntity, TUpdateEntityCommand>(entityCommand)));
        }

        public async Task Delete<TEntity, TDeleteEntityCommand>(TDeleteEntityCommand entityCommand) where TEntity : INamedEntity
                                                                                              where TDeleteEntityCommand : IDeleteEntityCommand<TEntity> {
            _logger.LogInformation($"Issuing action to delete { typeof(TEntity).Name}(s) ...");
            await Task.Run(() => _dispatcher.Dispatch(new DeleteEntityAction<TEntity, TDeleteEntityCommand>(entityCommand)));
        }
    }
}
