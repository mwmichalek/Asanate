using MediatR;
using System;
using FluentResults;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mwm.Asanate.Application.Shared.Commands;
using Mwm.Asanate.Domain;
using Microsoft.Extensions.Logging;
using Mwm.Asanate.Application.Interfaces.Persistance;
using Mwm.Asanate.Application.Utils;

namespace Mwm.Asanate.Application.Initiatives.Commands {
    public class InitiativeAdd {

        public class Command : IAddEntityCommand<Initiative> {

            public string Name { get; set; }

            public string? ExternalId { get; set; }

            public bool? IsArchived { get; set; }

            public int? PercentCompleted { get; set; }

            public string? Notes { get; set; }

            public DateTime? CompletedDate { get; set; }

            public DateTime? DueDate { get; set; }

            public DateTime? StartedDate { get; set; }

            public bool? IsComplete { get; set; }


            public int? ProjectId { get; set; }

            public int? AssignedToId { get; set; }

        }

        public class Handler : RequestHandler<Command, Result> {

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

            protected override Result Handle(Command command) {
                return CreateInitiative(command).ToResult(); 
            }

            private Result<int> CreateInitiative(Command command) {
                if (string.IsNullOrEmpty(command.Name))
                    return Result.Fail("Initiative Name can't be null.");
                if (!command.ProjectId.HasValue)
                    return Result.Fail("Initiative ProjectId must be set.");

                try {
                    var initiative = new Initiative {
                        ProjectId = command.ProjectId.Value,
                        Name = command.Name,
                        ModifiedDate = DateTime.Now
                    };
                    _initiativeRepository.Add(initiative);
                    _initiativeRepository.Save();
                    return Result.Ok(initiative.Id).WithSuccess(initiative.ToSuccess(ResultAction.Add));
                       
                } catch (Exception ex) {
                    _logger.LogError($"Initiative Add Failure: {ex}");
                    return Result.Fail(new Error("Unable to create Initiative").CausedBy(ex));
                }
            }

        }
    }
}




