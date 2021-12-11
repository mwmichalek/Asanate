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

namespace Mwm.MyQ.Application.Tsks.Commands {
    public partial class TskDelete {

        public class Handler : IRequestHandler<Command, Result<int>> {

            private ILogger<Handler> _logger;

            private readonly IRepository<Tsk> _tskRepository;

            public Handler(ILogger<Handler> logger,
                           IRepository<Tsk> tskRepository) {
                _logger = logger;
                _tskRepository = tskRepository;
            }

            public async Task<Result<int>> Handle(Command command, CancellationToken cancellationToken) {
                if (command.Id == 0)
                    return Result.Fail("Id can't be zero.");

                var tskResult = await DeleteTsk(command);

                return tskResult;
            }

            private async Task<Result<int>> DeleteTsk(Command command) {
                try {
                    var tsk = _tskRepository.Get(command.Id);

                    if (tsk == null)
                        return Result.Fail(new Error($"Couldn't locate existing Tsk with Id:{command.Id}."));

                    _tskRepository.Remove(tsk);
                    await _tskRepository.SaveAsync();
                    _logger.LogInformation($"Tsk Deleted: {tsk.Name}");
                    return Result.Ok(tsk.Id).WithSuccess(tsk.ToSuccess(ResultAction.Delete));
                } catch (Exception ex) {
                    _logger.LogError($"Tsk Deletion Failure: {ex}");
                    return Result.Fail(new Error("Unable to delete Tsk").CausedBy(ex));
                }

            }
        }

        
    }
}
