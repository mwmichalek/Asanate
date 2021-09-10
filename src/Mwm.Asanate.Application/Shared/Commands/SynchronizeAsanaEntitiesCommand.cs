using FluentResults;
using MediatR;
using Mwm.Asana.Model;
using Mwm.Asana.Service;
using Mwm.Asanate.Application.Interfaces.Persistance;
using Mwm.Asanate.Domain;
using System;

namespace Mwm.Asanate.Application.Shared.Commands {
    public class SynchronizeAsanaEntitiesCommand {

        public class Command : IRequest<Result> {
            public DateTime? Since { get; set; }

        }

        public class Handler : RequestHandler<Command, Result> {

            private readonly IRepository<Project> _projectRepository;
            private readonly IAsanaService<AsanaProject> _asanaProjectService;

            public Handler(IRepository<Project> projectRepository, IAsanaService<AsanaProject> asanaProjectService) {
                _projectRepository = projectRepository;
                _asanaProjectService = asanaProjectService;
            }

            protected override Result Handle(Command command) {
                var projectResults = _asanaProjectService.RetrieveAll().Result;

                if (projectResults.IsSuccess) {
                    var requiresSaving = false;
                    foreach (var asanaProject in projectResults.Value) {
                        _projectRepository.Add(new Project {
                            Name = asanaProject.Name
                        });
                        requiresSaving = true;
                    }

                    if (requiresSaving) _projectRepository.Save();


                    return Result.Ok();
                }

                return Result.Fail("Unable to retreive Asana Projects");
            }
        }
    }
}






