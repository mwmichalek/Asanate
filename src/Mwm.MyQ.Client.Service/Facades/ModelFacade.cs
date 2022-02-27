using Fluxor;
using Microsoft.Extensions.Logging;
using Mwm.MyQ.Application.Shared.Commands;
using Mwm.MyQ.Client.Service.Models;
using Mwm.MyQ.Client.Service.Store.Features.ModelFilters;
using Mwm.MyQ.Client.Service.Store.Features.Shared.Actions;
using Mwm.MyQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mwm.MyQ.Client.Service.Facades;
public class ModelFacade {

    private readonly ILogger<ModelFacade> _logger;
    private readonly IDispatcher _dispatcher;

    public ModelFacade(ILogger<ModelFacade> logger, IDispatcher dispatcher) =>
        (_logger, _dispatcher) = (logger, dispatcher);

    public void SetFilter<TModel, TEntity>(ModelFilter<TModel, TEntity> modelFilter) where TModel : EntityModel<TEntity>
                                                                                     where TEntity : INamedEntity {
        _logger.LogInformation($"Issuing action to set { modelFilter.GetType().Name} ...");
        _dispatcher.Dispatch(new FilterModelAction<TModel, TEntity>(modelFilter));
    }

    public void Edit<TModel, TEntity>(TModel model) where TModel : EntityModel<TEntity>
                                                    where TEntity : INamedEntity {
        _logger.LogInformation($"Issuing action to edit { model.Name} ...");
        _dispatcher.Dispatch(new EditingModelAction<TModel, TEntity>(model));
    }

}