using MediatR;
using Microsoft.Extensions.Logging;
using Mwm.MyQ.Application.Interfaces.Persistance;
using Mwm.MyQ.Application.Shared.Workflows;
using Mwm.MyQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mwm.MyQ.Application.Initiatives.Workflows {
    public class InitiativeSummaryUpdate : INotificationHandler<EntityCommandSuccessEvent<Tsk>> {

        private ILogger<InitiativeSummaryUpdate> _logger;
        private IRepository<Initiative> _initiativeRepository;
        private IRepository<Tsk> _tskRepository;

        public InitiativeSummaryUpdate(ILogger<InitiativeSummaryUpdate> logger, 
                                       IRepository<Initiative> initiativeRepository, 
                                       IRepository<Tsk> tskRepository) {
            _logger = logger;
            _initiativeRepository = initiativeRepository;
            _tskRepository = tskRepository;
        }

        public async Task Handle(EntityCommandSuccessEvent<Tsk> successEvent, CancellationToken cancellationToken) {
            var initiative = _initiativeRepository.Get(successEvent.Entity.InitiativeId);
            var tsksFromThisInitiative = await _tskRepository.WhereAsync(t => t.InitiativeId == initiative.Id);

            _logger.LogInformation($"Updating progress for Initiate: {initiative.Name}, Task Count: {tsksFromThisInitiative.Count}");
            

            foreach (var tsk in tsksFromThisInitiative) {
                //_logger.LogInformation($"Task in Initiative: {tsk.Id} {tsk.Name}");
            }
        }

    }

}
