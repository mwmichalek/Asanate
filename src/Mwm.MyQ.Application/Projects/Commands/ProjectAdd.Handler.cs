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
    public partial class ProjectAdd {

        public class Handler : IRequestHandler<Command, Result<int>> {

            private ILogger<Handler> _logger;

            private readonly IRepository<Project> _projectRepository;

            public Handler(ILogger<Handler> logger,
                           IRepository<Project> projectRepository) {
                _logger = logger;
                _projectRepository = projectRepository;
            }

            public async Task<Result<int>> Handle(Command command, CancellationToken cancellationToken) {
                return await CreateProject(command);
            }

            private async Task<Result<int>> CreateProject(Command command) {
                if (string.IsNullOrEmpty(command.Name))
                    return Result.Fail("Project Name can't be null.");
                if (command.CompanyId == 0)
                    return Result.Fail("Project CompanyId must be set.");

                try {
                    var project = new Project {
                        Name = command.Name,
                        Color = command.Color, 
                        CompanyId = command.CompanyId
                    };
                    _projectRepository.Add(project);
                    await _projectRepository.SaveAsync();
                    _logger.LogInformation($"Project Added: {project.Name}");
                    return Result.Ok(project.Id).WithSuccess(project.ToSuccess(ResultAction.Add));
                       
                } catch (Exception ex) {
                    _logger.LogError($"Project Add Failure: {ex}");
                    return Result.Fail(new Error("Unable to create Project").CausedBy(ex));
                }
            }
        }
    }
}




