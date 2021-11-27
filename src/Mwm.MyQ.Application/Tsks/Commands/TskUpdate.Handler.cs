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

namespace Mwm.MyQ.Application.Tsks.Commands {
    public partial class TskUpdate {

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
                    return Result.Fail("Tsk Name can't be null.");

                var tskResult = await UpdateTsk(command);

                return tskResult;
            }

            private async Task<Result<int>> UpdateTsk(Command command) {
                try {
                    var tsk = _tskRepository.Get(command.Id);

                    if (tsk == null)
                        return Result.Fail(new Error($"Couldn't locate existing Tsk with Id:{command.Id}."));

                    if (command.Name != null) tsk.Name = command.Name;
                    //if (!string.IsNullOrEmpty(command.ExternalId)) tsk.ExternalId = command.ExternalId;
                    tsk.Status = command.Status;
                    if (command.IsArchived.HasValue) tsk.IsArchived = command.IsArchived.Value;   
                    if (command.IsCompleted.HasValue) tsk.IsCompleted = command.IsCompleted.Value; 
                    if (command.DurationEstimate.HasValue) tsk.DurationEstimate = command.DurationEstimate.Value;   
                    if (command.DurationCompleted.HasValue) tsk.DurationCompleted = command.DurationCompleted.Value;
                    if (!string.IsNullOrEmpty(command.Notes)) tsk.Notes = command.Notes;
                    if (command.DueDate.HasValue) tsk.DueDate = command.DueDate.Value;
                    if (command.StartDate.HasValue) tsk.StartDate = command.StartDate.Value;
                    if (command.StartedDate.HasValue) tsk.StartedDate = command.StartedDate.Value;
                    if (command.CompletedDate.HasValue) tsk.CompletedDate = command.CompletedDate.Value;
                    if (command.AssignedToId.HasValue) tsk.AssignedToId = command.AssignedToId.Value;
                    // ?
                    if (command.CreatedById.HasValue) tsk.CreatedById = command.CreatedById.Value;
                    if (command.ModifiedById.HasValue) tsk.ModifiedById = command.ModifiedById.Value;
                    if (command.InitiativeId.HasValue) tsk.InitiativeId = command.InitiativeId.Value;

                    await _tskRepository.SaveAsync();
                    _logger.LogInformation($"Tsk Updated: {tsk.Name}");
                    return Result.Ok(tsk.Id).WithSuccess(tsk.ToSuccess(ResultAction.Update));
                } catch (Exception ex) {
                    _logger.LogError($"Tsk Update Failure: {ex}");
                    return Result.Fail(new Error("Unable to update Tsk").CausedBy(ex));
                }

            }
        }

        
    }
}
