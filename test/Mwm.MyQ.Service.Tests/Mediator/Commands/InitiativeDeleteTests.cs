using MediatR;
using Microsoft.EntityFrameworkCore;
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
using Mwm.MyQ.Application.Initiatives.Commands;

namespace Mwm.MyQ.Service.Tests.Mediator.Commands {

    [Collection("Generic")]
    public class InitiativeDeleteTests {
        private readonly IMediator _mediator;
        private readonly IDatabaseContext _databaseContext;
        private readonly ITestOutputHelper _output;

        public InitiativeDeleteTests(IMediator mediator, IDatabaseContext databaseContext, ITestOutputHelper output) {
            _databaseContext = databaseContext;
            _mediator = mediator;
            _output = output;
        }

        [Fact]
        public async Task DeleteInitiative() {
            var lastId = _databaseContext.Set<Initiative>().OrderByDescending(t => t.Id).Select(t => t.Id).FirstOrDefault();
            var command = new InitiativeDelete.Command {
                Id = lastId
            };

            var result = await _mediator.Send(command);
            Assert.True(result.IsSuccess, $"Delete Initiative failed: {result}");
            Assert.Equal(result.Value, lastId);

            _output.WriteLine(result.ToString());
        }

    }
}
