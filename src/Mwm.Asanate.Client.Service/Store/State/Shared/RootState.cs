using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mwm.Asanate.Client.Service.Store.State.Shared {
    public abstract class RootState {
        public RootState(bool isLoading, string? currentErrorMessage) =>
            (IsLoading, CurrentErrorMessage) = (isLoading, currentErrorMessage);

        public bool IsLoading { get; }

        public string? CurrentErrorMessage { get; }

        public bool HasCurrentErrors => !string.IsNullOrWhiteSpace(CurrentErrorMessage);

    }
}
