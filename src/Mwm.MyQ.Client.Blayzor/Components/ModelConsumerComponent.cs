using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using Mwm.MyQ.Client.Service.Models;
using Mwm.MyQ.Client.Service.Store.Features.Shared.Helpers;
using Mwm.MyQ.Client.Service.Store.State.Shared;
using Mwm.MyQ.Domain;
using System.Threading.Tasks;

namespace Mwm.MyQ.Client.Blayzor.Components;

public abstract class ModelConsumerComponent<TModel, TEntity> : FluxorComponent where TModel : EntityModel<TEntity>
                                                                                where TEntity : INamedEntity {
    [Inject]
    public IState<ModelState<TModel, TEntity>> ModelsState { get; set; }

    [Inject]
    public IActionSubscriber ActionSubscriber { get; set; }

    public bool HasValues() => ModelsState.HasValue(false);

    public bool HasErrors() => ModelsState.HasErrors();

    public bool IsLoading() => ModelsState.IsLoading();

    protected override async Task OnInitializedAsync() {
        ModelsState.StateChanged += async (s, e) => await HandleModelsLoaded();
        await base.OnInitializedAsync();
    }

    protected virtual Task HandleModelsLoaded() => Task.CompletedTask;

}
