using MediatR;
using System;
using FluentResults;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mwm.MyQ.Application.Shared.Commands;
using Mwm.MyQ.Domain;
using Microsoft.Extensions.Logging;
using Mwm.MyQ.Application.Interfaces.Persistance;
using Mwm.MyQ.Application.Utils;
using System.Threading;

namespace Mwm.MyQ.Application.Initiatives.Commands {
    public partial class InitiativeAdd {

        public class Handler : IRequestHandler<Command, Result<int>> {

            private ILogger<Handler> _logger;

            private readonly IRepository<User> _userRepository;
            private readonly IRepository<Initiative> _initiativeRepository;

            public Handler(ILogger<Handler> logger,
                           IRepository<User> userRepository,
                           IRepository<Initiative> initiativeRepository,
                           IRepository<Tsk> tskRepository) {
                _logger = logger;
                _userRepository = userRepository;
                _initiativeRepository = initiativeRepository;
            }

            public async Task<Result<int>> Handle(Command command, CancellationToken cancellationToken) {
                return await CreateInitiative(command);
            }

            private async Task<Result<int>> CreateInitiative(Command command) {
                if (string.IsNullOrEmpty(command.Name))
                    return Result.Fail("Initiative Name can't be null.");
                if (!command.ProjectId.HasValue)
                    return Result.Fail("Initiative ProjectId must be set.");

                try {
                    var initiative = new Initiative {
                        ProjectId = command.ProjectId.Value,
                        ExternalId = command.ExternalId,
                        Name = command.Name,
                        ModifiedDate = DateTime.Now
                    };
                    _initiativeRepository.Add(initiative);
                    await _initiativeRepository.SaveAsync();
                    _logger.LogInformation($"Initiative Added: {initiative.Name}");
                    return Result.Ok(initiative.Id).WithSuccess(initiative.ToSuccess(ResultAction.Add));
                       
                } catch (Exception ex) {
                    _logger.LogError($"Initiative Add Failure: {ex}");
                    return Result.Fail(new Error("Unable to create Initiative").CausedBy(ex));
                }
            }
        }
    }
}




