using MediatR;
using Microsoft.EntityFrameworkCore;
using Mwm.MyQ.Application.Initiatives.Commands;
using Mwm.MyQ.Application.Tsks.Commands;
using Mwm.MyQ.Application.Utils;
using Mwm.MyQ.Data;
using Mwm.MyQ.Domain;
using Mwm.MyQ.Persistance.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Mwm.MyQ.Service.Tests.Mediator.Commands {

    [Collection("Generic")]
    public class InitiativeUpdateTests {
        private readonly IMediator _mediator;
        private readonly IDatabaseContext _databaseContext;
        private readonly ITestOutputHelper _output;

        public InitiativeUpdateTests(IMediator mediator, IDatabaseContext databaseContext, ITestOutputHelper output) {
            _databaseContext = databaseContext;
            _mediator = mediator;
            _output = output;

        }

        [Fact]
        public async Task UpdateSimpleInitiative() {
            var name = $"SimpleInitiative_{DateTime.Now}";
            var command = new InitiativeUpdate.Command {
                Id = 1,
                Name = name
            };

            var result = await _mediator.Send(command);
            Assert.True(result.IsSuccess, $"UpdateSimpleInitiative failed: {result}");
            Assert.True(result.HasSuccess<EntitySuccess<Initiative>>(t => t.Action == ResultAction.Update));
            Assert.True(result.HasSuccess<EntitySuccess<Initiative>>(t => t.Entity.Name == command.Name));

            var initiative = _databaseContext.Initiatives.Find(result.Value);
            Assert.Equal(command.Name, initiative.Name);

            _output.WriteLine(result.ToString());
        }

    }
}
