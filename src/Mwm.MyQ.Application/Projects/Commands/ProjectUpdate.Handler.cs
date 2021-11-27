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

namespace Mwm.MyQ.Application.Projects.Commands {
    public partial class ProjectUpdate {

        public class Handler : IRequestHandler<Command, Result<int>> {

            private ILogger<Handler> _logger;

            private readonly IRepository<Project> _projectRepository;

            public Handler(ILogger<Handler> logger,
                           IRepository<Project> projectRepository) {
                _logger = logger;
                _projectRepository = projectRepository;
            }

            public async Task<Result<int>> Handle(Command command, CancellationToken cancellationToken) {
                if (command.Id == 0)
                    return Result.Fail("Project Id must be set.");
                if (string.IsNullOrEmpty(command.Name))
                    return Result.Fail("Project Name can't be null.");
                if (command.CompanyId == 0)
                    return Result.Fail("Project CompanyId must be set.");

                return await UpdateProject(command);
            }

            private async Task<Result<int>> UpdateProject(Command command) {
                var project = _projectRepository.Get(command.Id);

                if (project == null)
                    return Result.Fail(new Error($"Couldn't locate existing Project with Id:{command.Id}."));

                if (command.Name != null) project.Name = command.Name;
                if (command.Color != null) project.Color = command.Color;
                if (command.CompanyId != 0) project.CompanyId = command.CompanyId;

                try {
                    await _projectRepository.SaveAsync();
                    _logger.LogInformation($"Project Updated: {project.Name}");
                    return Result.Ok(project.Id).WithSuccess(project.ToSuccess(ResultAction.Update));  
                } catch (Exception ex) {
                    _logger.LogError($"Project Update Failure: {ex}");
                    return Result.Fail(new Error("Unable to update Project").CausedBy(ex));
                }
            }
        }
    }
}




