using Mwm.Asanate.Client.Blazor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mwm.Asanate.Client.Blazor.Store.State {
    public class TodosState : RootState {
        public TodosState(bool isLoading, string? currentErrorMessage, IEnumerable<TodoDto>? currentTodos, TodoDto? currentTodo)
            : base(isLoading, currentErrorMessage) {
            CurrentTodos = currentTodos;
            CurrentTodo = currentTodo;
        }

        public IEnumerable<TodoDto>? CurrentTodos { get; }

        public TodoDto? CurrentTodo { get; }
    }
}
