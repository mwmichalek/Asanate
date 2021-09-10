using FluentResults;
using MediatR;
using Mwm.Asana.Model;
using Mwm.Asana.Service;
using Mwm.Asanate.Application.Interfaces.Persistance;
using Mwm.Asanate.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.Asanate.Application.Shared.Commands {
    public class SynchronizeAsanaEntitiesCommand {

        public class Command : IRequest<Result> {
            public DateTime? Since { get; set; }

        }

        public class Handler : RequestHandler<Command, Result> {

            private readonly IRepository<Project> _projectRepository;


            public Handler(IRepository<Project> projectRepository, IAsanaService<AsanaProject> _asanaProjectService) {
                _projectRepository = projectRepository;
            }

            protected override Result Handle(Command command) {
                return Result.Ok();
            }
        }
    }
}






