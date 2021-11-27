using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using Mwm.MyQ.Application.Interfaces.Persistance;
using Mwm.MyQ.Application.Shared.Commands;
using Mwm.MyQ.Application.Utils;
using Mwm.MyQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mwm.MyQ.Application.Initiatives.Commands {
    public partial class InitiativeDelete {

        public class Handler : IRequestHandler<Command, Result<int>> {

            private ILogger<Handler> _logger;

            private readonly IRepository<Initiative> _initiativeRepository;

            public Handler(ILogger<Handler> logger,
                           IRepository<Initiative> initiativeRepository) {
                _logger = logger;
                _initiativeRepository = initiativeRepository;
            }

            public async Task<Result<int>> Handle(Command command, CancellationToken cancellationToken) {
                if (command.Id == 0)
                    return Result.Fail("Id can't be zero.");

                var tskResult = await DeleteInitiative(command);

                return tskResult;
            }

            private async Task<Result<int>> DeleteInitiative(Command command) {
                try {
                    var initiative = _initiativeRepository.Get(command.Id);

                    if (initiative == null)
                        return Result.Fail(new Error($"Couldn't locate existing Initiative with Id:{command.Id}."));

                    _initiativeRepository.Remove(initiative);
                    await _initiativeRepository.SaveAsync();
                    _logger.LogInformation($"Initiative Deleted: {initiative.Name}");
                    return Result.Ok(initiative.Id).WithSuccess(initiative.ToSuccess(ResultAction.Delete));
                } catch (Exception ex) {
                    _logger.LogError($"Initiative Deletion Failure: {ex}");
                    return Result.Fail(new Error("Unable to delete Initiative").CausedBy(ex));
                }

            }
        }

        
    }
}
