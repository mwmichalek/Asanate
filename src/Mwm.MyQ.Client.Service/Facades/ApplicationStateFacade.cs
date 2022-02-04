using Fluxor;
using Microsoft.Extensions.Logging;
using Mwm.MyQ.Client.Service.Store.Features.Settings;

namespace Mwm.MyQ.Client.Service.Facades;
    public class ApplicationStateFacade {

        private readonly ILogger<ApplicationStateFacade> _logger;
        private readonly IDispatcher _dispatcher;

        public ApplicationStateFacade(ILogger<ApplicationStateFacade> logger, IDispatcher dispatcher) =>
            (_logger, _dispatcher) = (logger, dispatcher);

        public void Set<TApplicationSetting>(TApplicationSetting setting) where TApplicationSetting : IApplicationSetting {
            _logger.LogInformation($"Issuing action to set { setting.GetType().Name} ...");
            _dispatcher.Dispatch(new SetApplicationSettingAction(setting));
        }

    }
