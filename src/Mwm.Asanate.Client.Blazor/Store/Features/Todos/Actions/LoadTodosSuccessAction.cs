using Mwm.Asanate.Client.Blazor.Models.Todos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mwm.Asanate.Client.Blazor.Store.Features.Todos.Actions {
    public class LoadTodosSuccessAction {
        public LoadTodosSuccessAction(IEnumerable<TodoDto> todos) =>
            Todos = todos;

        public IEnumerable<TodoDto> Todos { get; }
    }
}
