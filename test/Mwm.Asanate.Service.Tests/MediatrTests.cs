using MediatR;
using Mwm.Asanate.Application.Shared.Commands;
using Mwm.Asanate.Data;
using Mwm.Asanate.Data.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Mwm.Asanate.Service.Tests {

    [Collection("Generic")]
    public class MediatrTests {
        private readonly IMediator _mediator;
        private readonly DatabaseContext _databaseContext;
        private readonly ITestOutputHelper _output;


        public MediatrTests(IMediator mediator, DatabaseContext databaseContext, ITestOutputHelper output) {
            _databaseContext = databaseContext;
            _mediator = mediator;
            _output = output;

            _databaseContext.RecreateDatabase();
        }

        [Fact]
        public async Task SynchronizeAsanaEntities() {
            var command = new SynchronizeAsanaEntitiesCommand.Command {
                Since = DateTime.Now.AddDays(-10)
            };

            var result = await _mediator.Send(command);

            var projects = _databaseContext.Projects.ToList();

            Assert.True(projects.Count > 0);

        }
    }
}

