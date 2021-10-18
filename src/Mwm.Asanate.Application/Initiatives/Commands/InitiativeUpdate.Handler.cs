using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using Mwm.Asanate.Application.Interfaces.Persistance;
using Mwm.Asanate.Application.Shared.Commands;
using Mwm.Asanate.Application.Utils;
using Mwm.Asanate.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mwm.Asanate.Application.Initiatives.Commands {
    public partial class InitiativeUpdate {

        public class Handler : IRequestHandler<Command, Result<int>> {

            private ILogger<Handler> _logger;

            private readonly IRepository<User> _userRepository;
            private readonly IRepository<Initiative> _initiativeRepository;
            private readonly IRepository<Tsk> _tskRepository;

            public Handler(ILogger<Handler> logger,
                           IRepository<User> userRepository,
                           IRepository<Initiative> initiativeRepository,
                           IRepository<Tsk> tskRepository) {
                _logger = logger;
                _userRepository = userRepository;
                _initiativeRepository = initiativeRepository;
                _tskRepository = tskRepository;
            }

            public async Task<Result<int>> Handle(Command command, CancellationToken cancellationToken) {
                if (command.Id == 0)
                    return Result.Fail("Id can't be zero.");
                if (string.IsNullOrEmpty(command.Name))
                    return Result.Fail("Initiative Name can't be null.");

                var initiativeResult = await UpdateInitiative(command);

                return initiativeResult;
            }

            private async Task<Result<int>> UpdateInitiative(Command command) {
                try {
                    var initiative = _initiativeRepository.Get(command.Id);

                    if (initiative == null)
                        return Result.Fail(new Error($"Couldn't locate existing Initiative with Id:{command.Id}."));

                    if (command.Name != null) initiative.Name = command.Name;
                    if (!string.IsNullOrEmpty(command.ExternalId)) initiative.ExternalId = command.ExternalId;
                    //if (command.IsArchived.HasValue) initiative.IsArchived = command.IsArchived.Value;
                    //if (command.IsCompleted.HasValue) initiative.IsCompleted = command.IsCompleted.Value;
                    //if (command.DurationEstimate.HasValue) tsk.DurationEstimate = command.DurationEstimate.Value;
                    //if (command.DurationCompleted.HasValue) tsk.DurationCompleted = command.DurationCompleted.Value;
                    //if (!string.IsNullOrEmpty(command.Notes)) tsk.Notes = command.Notes;
                    //if (command.DueDate.HasValue) tsk.DueDate = command.DueDate.Value;
                    //if (command.StartDate.HasValue) tsk.StartDate = command.StartDate.Value;
                    //if (command.StartedDate.HasValue) tsk.StartedDate = command.StartedDate.Value;
                    //if (command.CompletedDate.HasValue) tsk.CompletedDate = command.CompletedDate.Value;
                    //if (command.AssignedToId.HasValue) tsk.AssignedToId = command.AssignedToId.Value;
                    //// ?
                    //if (command.CreatedById.HasValue) tsk.CreatedById = command.CreatedById.Value;
                    //if (command.ModifiedById.HasValue) tsk.ModifiedById = command.ModifiedById.Value;
                    //if (command.InitiativeId.HasValue) tsk.InitiativeId = command.InitiativeId.Value;

                    await _initiativeRepository.SaveAsync();
                    _logger.LogInformation($"Initiative Updated: {initiative.Name}");
                    return Result.Ok(initiative.Id).WithSuccess(initiative.ToSuccess(ResultAction.Update));
                } catch (Exception ex) {
                    _logger.LogError($"Initiative Update Failure: {ex}");
                    return Result.Fail(new Error("Unable to update Initiative").CausedBy(ex));
                }
            }

        }
    }
}
