using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mwm.MyQ.Application.Asana.Commands;
using Mwm.MyQ.Persistance.Shared;
using System.Threading.Tasks;

namespace Mwm.MyQ.Server.Controllers {

    [ApiController]
    [Route("CreateNewDatabaseAndRunSynch")]
    public class DbController : ControllerBase {

        private IDatabaseContext _databaseContext;
        private IMediator _mediator;

        public DbController(IMediator mediator, IDatabaseContext databaseContext) => (_mediator, _databaseContext) = (mediator, databaseContext);

        [HttpPost]
        public void CreateNewDatabaseAndRunSynch() {
            Task.Run(async () => {
                _databaseContext.RecreateDatabase();

                var command = new AsanaEntitiesSynch.Command {
                    //Since = DateTime.Now.AddDays(-10)
                };

                await _mediator.Send(command);
            });
        }
    }
}
