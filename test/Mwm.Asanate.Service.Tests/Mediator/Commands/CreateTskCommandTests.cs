using MediatR;
using Mwm.Asanate.Application.Tsks.Commands;
using Mwm.Asanate.Application.Utils;
using Mwm.Asanate.Data;
using Mwm.Asanate.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Mwm.Asanate.Service.Tests.Mediator.Commands {

    [Collection("Generic")]
    public class CreateTskCommandTests {
        private readonly IMediator _mediator;
        private readonly DatabaseContext _databaseContext;
        private readonly ITestOutputHelper _output;

        public CreateTskCommandTests(IMediator mediator, DatabaseContext databaseContext, ITestOutputHelper output) {
            _databaseContext = databaseContext;
            _mediator = mediator;
            _output = output;

        }

        [Fact]
        public async Task AddSimpleTriageAsync() {
            var name = $"SimpleTsk_{DateTime.Now}";
            var command = new CreateTskCommand.Command {
                Name = name
            };

            var result = await _mediator.Send(command);
            Assert.True(result.IsSuccess, $"AddSimpleTriageAsync failed: {result}");

            _output.WriteLine(result.ToString());
        }

        [Fact]
        public async Task AddCompleteTriageAsync() {
            var name = $"ComplexTsk_{DateTime.Now}";
            var command = new CreateTskCommand.Command {
                Name = name
            };

            var result = await _mediator.Send(command);

            Assert.True(result.IsSuccess, $"AddSimpleTriageAsync failed: {result}");

            Assert.True(result.HasSuccess<EntitySuccess<Tsk>>(t => t.Entity.Name == name));


            _output.WriteLine(result.ToString());
        }

    }
}
