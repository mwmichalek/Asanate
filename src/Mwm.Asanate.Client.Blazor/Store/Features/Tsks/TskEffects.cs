using Microsoft.Extensions.Logging;
using Mwm.Asanate.Client.Blazor.Store.Features.Shared.Effects;
using Mwm.Asanate.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mwm.Asanate.Client.Blazor.Store.Features.Tsks {
    public class LoadTsksEffect : LoadEffect<Tsk> {

        public LoadTsksEffect(ILogger<LoadEffect<Tsk>> logger, HttpClient httpClient) : base(logger, httpClient) { }

    }

    //public class LoadTodosEffect : Effect<LoadTodosAction> {
    //    private readonly ILogger<LoadTodosEffect> _logger;
    //    private readonly HttpClient _httpClient;

    //    public LoadTodosEffect(ILogger<LoadTodosEffect> logger, HttpClient httpClient) =>
    //        (_logger, _httpClient) = (logger, httpClient);

    //    public override async Task HandleAsync(LoadTodosAction action, IDispatcher dispatcher) {
    //        try {
    //            _logger.LogInformation("Loading todos...");

    //            // Add a little extra latency for dramatic effect...
    //            await Task.Delay(TimeSpan.FromMilliseconds(1000));
    //            var todosResponse = await _httpClient.GetFromJsonAsync<IEnumerable<TodoDto>>("todos");

    //            _logger.LogInformation("Todos loaded successfully!");
    //            dispatcher.Dispatch(new LoadTodosSuccessAction(todosResponse));
    //        } catch (Exception e) {
    //            _logger.LogError($"Error loading todos, reason: {e.Message}");
    //            dispatcher.Dispatch(new LoadTodosFailureAction(e.Message));
    //        }

    //    }
    //}
}
