﻿using Fluxor;
using Microsoft.Extensions.Logging;
using Mwm.Asanate.Client.Service.Storage;
using Mwm.Asanate.Client.Service.Store.Features.Shared.Actions;
using Mwm.Asanate.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Mwm.Asanate.Application.Tsks.Commands;
using Mwm.Asanate.Application.Shared.Commands;

namespace Mwm.Asanate.Client.Service.Store.Features.Shared.Effects {
    public abstract class AddEffect<TEntity> : Effect<AddAction<TEntity>> where TEntity : INamedEntity {

        protected readonly ILogger<AddEffect<TEntity>> _logger;
        protected readonly IEntityStorage _entityStorage;

        public AddEffect(ILogger<AddEffect<TEntity>> logger, IEntityStorage entityStorage) =>
            (_logger, _entityStorage) = (logger, entityStorage);

        public override async Task HandleAsync(AddAction<TEntity> action, IDispatcher dispatcher) {
            var entityName = typeof(TEntity).Name;
            try {
                _logger.LogInformation($"Adding {entityName} ...");

                //var tskCommand = new TskAdd.Command {
                //    Name = action.Entity.Name
                //};
                //var id = await _entityStorage.Add<TEntity, IAddEntityCommand<TEntity>>(tskCommand);
                _logger.LogInformation($"Added {entityName} successfully!");
                dispatcher.Dispatch(new AddSuccessAction<TEntity>(0));
            } catch (Exception e) {
                _logger.LogError($"Error adding {entityName}(s), reason: {e}");
                dispatcher.Dispatch(new AddFailureAction<TEntity>(e.Message));
            }

        }
    }
}
