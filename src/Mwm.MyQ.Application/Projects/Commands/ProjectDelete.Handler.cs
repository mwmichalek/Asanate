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

namespace Mwm.MyQ.Application.Projects.Commands {
    public partial class ProjectDelete {

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
                    return Result.Fail("Id can't be zero.");

                var tskResult = await DeleteProject(command);

                return tskResult;
            }

            private async Task<Result<int>> DeleteProject(Command command) {
                try {
                    var project = _projectRepository.Get(command.Id);

                    if (project == null)
                        return Result.Fail(new Error($"Couldn't locate existing Project with Id:{command.Id}."));

                    _projectRepository.Remove(project);
                    await _projectRepository.SaveAsync();
                    _logger.LogInformation($"Project Deleted: {project.Name}");
                    return Result.Ok(project.Id).WithSuccess(project.ToSuccess(ResultAction.Delete));
                } catch (Exception ex) {
                    _logger.LogError($"Project Deletion Failure: {ex}");
                    return Result.Fail(new Error("Unable to delete Project").CausedBy(ex));
                }

            }
        }

        
    }
}
