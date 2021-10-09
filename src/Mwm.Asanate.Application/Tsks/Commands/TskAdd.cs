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


namespace Mwm.Asanate.Application.Tsks.Commands {

    public class TskAdd {

        public class Command : IAddEntityCommand<Tsk> {

            public string Name { get; set; }

            public string? ExternalId { get; set; } 

            public Status Status { get; set; } 

            public bool? IsArchived { get; set; }

            public string? Notes { get; set; } 

            public DateTime? CompletedDate { get; set; } 

            public DateTime? DueDate { get; set; } 

            public DateTime? StartedDate { get; set; } 

            public int? InitiativeId { get; set; } 

            //public string NewInitiativeName { get; set; }

            //public int? ProjectId { get; set; }

            public int? AssignedToId { get; set; } 

        }

        public class Handler : IRequestHandler<Command, Result> {

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

            public async Task<Result> Handle(Command command, CancellationToken cancellationToken) {

                if (string.IsNullOrEmpty(command.Name))
                    return Result.Fail("Tsk Name can't be null.");

                var initiativeResult = await FindOrCreateInitiative(command);

                if (initiativeResult.IsFailed)
                    return initiativeResult.ToResult();
                command.InitiativeId = initiativeResult.Value;

                var tskResult = await CreateTsk(command);

                return Result.Merge(initiativeResult.ToResult(), tskResult.ToResult());
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
                        ExternalId = command.ExternalId,
                        Notes = command.Notes,
                        CompletedDate = command.CompletedDate,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        DueDate = command.DueDate,
                        StartedDate = command.StartedDate,
                        Status = command.Status,
                        InitiativeId = command.InitiativeId.Value,
                        AssignedToId = User.MeId
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
