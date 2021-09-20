using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using Mwm.Asanate.Application.Interfaces.Persistance;
using Mwm.Asanate.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.Asanate.Application.Tsks.Commands {
    public class CreateTskCommand {

        public class Command : IRequest<Result> {

            public string ExternalId { get; set; }

            public Status Status { get; set; } = Status.Open;

            public bool? IsArchived { get; set; }

            public string? Notes { get; set; }

            public DateTime? CompletedDate { get; set; }

            public DateTime? DueDate { get; set; }

            public DateTime? StartDate { get; set; }

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

                Initiative initiative;

                if (command.InitiativeId.HasValue) 
                    initiative = _initiativeRepository.Get(command.InitiativeId.Value);
                else if (!string.IsNullOrEmpty(command.NewInitiativeName) &&
                         command.ProjectId.HasValue) {
                    initiative = new Initiative {
                        ProjectId = command.ProjectId.Value,
                        Name = command.NewInitiativeName
                    };
                    _initiativeRepository.Add(initiative);
                    _initiativeRepository.Save();
                } else {
                    initiative = _initiativeRepository.GetAll().SingleOrDefault(i => i.Name == Initiative.DefaultInitiativeName &&
                                                                                     i.Project.Name == Project.DefaultProjectName);
                }

                //TODO:(MWM) Set other Tsk properties.


                return Result.Ok();
            }
        }
    }
    }
