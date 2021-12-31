using Fluxor;
using Microsoft.Extensions.Logging;
using Mwm.MyQ.Application.Shared.Commands;
using Mwm.MyQ.Client.Service.Store.Features.Settings;
using Mwm.MyQ.Client.Service.Store.Features.Shared.Actions;
using Mwm.MyQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mwm.MyQ.Client.Service.Facades {
    public class ApplicationStateFacade {

        private readonly ILogger<ApplicationStateFacade> _logger;
        private readonly IDispatcher _dispatcher;

        public ApplicationStateFacade(ILogger<ApplicationStateFacade> logger, IDispatcher dispatcher) =>
            (_logger, _dispatcher) = (logger, dispatcher);

        public void Set(IApplicationSetting setting) {
            _logger.LogInformation($"Issuing action to set { setting.GetType().Name} ...");
            _dispatcher.Dispatch(new SetApplicationSettingAction(setting));
        }

    }
}
