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
using System.Threading.Tasks;

namespace Mwm.Asanate.Application.Tsks.Commands {

    public class TskAdd {

        public class Command : IAddEntityCommand<Tsk> {

            public string Name { get; set; }

            public string ExternalId { get; set; }

            public Status Status { get; set; } = Status.Open;

            public bool? IsArchived { get; set; }

            public string? Notes { get; set; }

            public DateTime? CompletedDate { get; set; }

            public DateTime? DueDate { get; set; }

            public DateTime? StartedDate { get; set; }

            public int? InitiativeId { get; set; }

            public string NewInitiativeName { get; set; }

            public int? ProjectId { get; set; }

            public int? AssignedToId { get; set; }

        }

        public class Handler : RequestHandler<Command, Result> {

            private ILogger<Handler> _logger;

            private readonly IRepository<User> _userRepository;
            private readonly IRepository<Initiative> _initiativeRepository;
            private readonly IRepository<Tsk> _tskRepository;


            public Handler(ILogger<Handler> logger,
                           IRepository<User> userRepository,
                           IRepository<Initiative> initiativeRepository,
                           IRepository<Tsk> tskRepository) {
                _userRepository = userRepository;
                _initiativeRepository = initiativeRepository;
                _tskRepository = tskRepository;
            }

            protected override Result Handle(Command command) {

                if (string.IsNullOrEmpty(command.Name))
                    return Result.Fail("Tsk Name can't be null.");

                var initiativeResult = FindOrCreateInitiative(command);

                if (initiativeResult.IsFailed)
                    return initiativeResult.ToResult();

                var tskResult = CreateTsk(command, initiativeResult.Value);

                return Result.Merge(initiativeResult.ToResult(), tskResult.ToResult());
            }

            private Result<int> FindOrCreateInitiative(Command command) {
                try {
                    if (command.InitiativeId.HasValue) {
                        var initiative = _initiativeRepository.Get(command.InitiativeId.Value);
                        return Result.Ok(initiative.Id).WithSuccess(initiative.ToSuccess(ResultAction.Find));

                    } else if (!string.IsNullOrEmpty(command.NewInitiativeName) &&
                                command.ProjectId.HasValue) {
                        var initiative = new Initiative {
                            ProjectId = command.ProjectId.Value,
                            Name = command.NewInitiativeName,
                            ModifiedDate = DateTime.Now
                        };
                        _initiativeRepository.Add(initiative);
                        _initiativeRepository.Save();
                        return Result.Ok(initiative.Id).WithSuccess(initiative.ToSuccess(ResultAction.Add));
                    } else {
                        // Default "Generic" - "Triage"
                        var initiative = _initiativeRepository.GetAll().SingleOrDefault(i => i.Name == Initiative.DefaultInitiativeName &&
                                                                                            i.Project.Name == Project.DefaultProjectName);
                        return Result.Ok(initiative.Id).WithSuccess(initiative.ToSuccess(ResultAction.Find));
                    }
                } catch (Exception ex) {
                    return Result.Fail(new Error("Unable to create Initiative").CausedBy(ex));
                }
            }

            private Result<int> CreateTsk(Command command, int initiativeId) {
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
                        InitiativeId = initiativeId,
                        AssignedToId = User.MeId
                    };
                    if (command.AssignedToId.HasValue) tsk.AssignedToId = command.AssignedToId.Value;
                    if (command.IsArchived.HasValue) tsk.IsArchived = command.IsArchived.Value;

                    _tskRepository.Add(tsk);
                    _tskRepository.Save();
                    return Result.Ok(tsk.Id).WithSuccess(tsk.ToSuccess(ResultAction.Add));
                } catch (Exception ex) {
                    return Result.Fail(new Error("Unable to create Tsk").CausedBy(ex));
                }
            }
        }
    }
}
