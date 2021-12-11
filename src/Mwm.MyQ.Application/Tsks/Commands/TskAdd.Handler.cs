using FluentResults;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using Mwm.MyQ.Application.Interfaces.Persistance;
using Mwm.MyQ.Application.Shared.Commands;
using Mwm.MyQ.Application.Shared.Workflows;
using Mwm.MyQ.Application.Utils;
using Mwm.MyQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Mwm.MyQ.Application.Tsks.Commands {

    public partial class TskAdd {

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

                if (string.IsNullOrEmpty(command.Name))
                    return Result.Fail("Tsk Name can't be null.");

                var initiativeResult = await FindOrCreateInitiative(command);

                if (initiativeResult.IsFailed)
                    return initiativeResult.ToResult();
                command.InitiativeId = initiativeResult.Value;
                var initiativeSuccess = initiativeResult.Successes.FirstOrDefault();

                var tskResult = await CreateTsk(command);
                tskResult.Successes.Add(initiativeSuccess);

                return tskResult;
            }

            private async Task<Result<int>> FindOrCreateInitiative(Command command) {
                try {
                    if (command.InitiativeId.HasValue) {
                        var initiative = await _initiativeRepository.GetAsync(command.InitiativeId.Value);
                        _logger.LogInformation($"Initiative Found: {initiative.Name}");
                        return Result.Ok(initiative.Id).WithSuccess(initiative.ToSuccess(ResultAction.Find));
                    } else {
                        // Default "Generic" - "Triage"
                        var initiative = await _initiativeRepository.SingleOrDefaultAsync(i => i.Name == Initiative.DefaultInitiativeName &&
                                                                                               i.Project.Name == Project.DefaultProjectName);
                        _logger.LogInformation($"Initiative Found: {initiative.Name}");
                        return Result.Ok(initiative.Id).WithSuccess(initiative.ToSuccess(ResultAction.Find));
                    }
                } catch (Exception ex) {
                    _logger.LogError($"Initiative Add/Find Failure: {ex}");
                    return Result.Fail(new Error("Unable to create Initiative").CausedBy(ex));
                }
            }


            private async Task<Result<int>> CreateTsk(Command command) {
                try {
                    var tsk = new Tsk {
                        Name = command.Name,
                        //ExternalId = command.ExternalId,
                        Status = command.Status,
                        IsArchived = command.IsArchived.HasValue ? command.IsArchived.Value : false,
                        IsCompleted = command.IsCompleted.HasValue ? command.IsCompleted.Value : false,
                        DurationEstimate = command.DurationEstimate,
                        DurationCompleted = command.DurationCompleted,
                        Notes = command.Notes,
                        
                        DueDate = command.DueDate,
                        StartDate = command.StartDate,
                        StartedDate = command.StartedDate,
                        CompletedDate = command.CompletedDate,
                        AssignedToId = command.AssignedToId,

                        CreatedDate = DateTime.Now,
                        CreatedById = command.CreatedById,
                        ModifiedDate = DateTime.Now,
                        ModifiedById = command.ModifiedById,

                        InitiativeId = command.InitiativeId.Value
                    };
                    if (command.AssignedToId.HasValue) tsk.AssignedToId = command.AssignedToId.Value;
                    if (command.IsArchived.HasValue) tsk.IsArchived = command.IsArchived.Value;
                    _tskRepository.Add(tsk);
                    await _tskRepository.SaveAsync();
                    _logger.LogInformation($"Tsk Added: {tsk.Name}");
                    return Result.Ok(tsk.Id).WithSuccess(tsk.ToSuccess(ResultAction.Add));
                } catch (Exception ex) {
                    _logger.LogError($"Tsk Addition Failure: {ex}");
                    return Result.Fail(new Error("Unable to create Tsk").CausedBy(ex));
                }
            }
        }
    }
}
