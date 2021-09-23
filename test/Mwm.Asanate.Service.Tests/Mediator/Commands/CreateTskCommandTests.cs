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
        public async Task AddSimpleTskToTriage() {
            var name = $"SimpleTsk_{DateTime.Now}";
            var command = new CreateTskCommand.Command {
                Name = name
            };

            var result = await _mediator.Send(command);
            Assert.True(result.IsSuccess, $"AddComplexTskToTriage failed: {result}");
            Assert.True(result.HasSuccess<EntitySuccess<Tsk>>(t => t.Action == ResultAction.Add));
            Assert.True(result.HasSuccess<EntitySuccess<Tsk>>(t => t.Entity.Name == command.Name));

            _output.WriteLine(result.ToString());
        }

        [Fact]
        public async Task AddComplexTskToTriage() {

            var command = new CreateTskCommand.Command {
                Name = $"ComplexTsk_{DateTime.Now}",
                Status = Status.Done,
                Notes = "Notes and notes and notes",
                StartedDate = DateTime.Today.AddDays(-2),
                DueDate = DateTime.Today.AddDays(5),
                CompletedDate = DateTime.Today.AddDays(3),
                IsArchived = true,
            };

            var result = await _mediator.Send(command);

            Assert.True(result.IsSuccess, $"AddComplexTskToTriage failed: {result}");

            Assert.True(result.HasSuccess<EntitySuccess<Tsk>>(t => t.Action == ResultAction.Add));
            Assert.True(result.HasSuccess<EntitySuccess<Tsk>>(t => t.Entity.Name == command.Name));
            Assert.True(result.HasSuccess<EntitySuccess<Tsk>>(t => t.Entity.Status == command.Status));
            Assert.True(result.HasSuccess<EntitySuccess<Tsk>>(t => t.Entity.Notes == command.Notes));
            Assert.True(result.HasSuccess<EntitySuccess<Tsk>>(t => t.Entity.DueDate == command.DueDate));
            Assert.True(result.HasSuccess<EntitySuccess<Tsk>>(t => t.Entity.StartedDate == command.StartedDate));
            Assert.True(result.HasSuccess<EntitySuccess<Tsk>>(t => t.Entity.CompletedDate == command.CompletedDate));
            Assert.True(result.HasSuccess<EntitySuccess<Tsk>>(t => t.Entity.IsArchived == command.IsArchived));

            Assert.True(result.HasSuccess<EntitySuccess<Initiative>>(t => t.Action == ResultAction.Find));
            Assert.True(result.HasSuccess<EntitySuccess<Initiative>>(t => t.Entity.Name == Initiative.DefaultInitiativeName &&
                                                                          t.Entity.Project.Name == Project.DefaultProjectName));

            _output.WriteLine(result.ToString());
        }

        [Fact]
        public async Task AddSimpleTskToNewInitiativeInExistingProject() {

            var command = new CreateTskCommand.Command {
                Name = $"SimpleTsk_{DateTime.Now}",
                Status = Status.Open,
                Notes = "Notes and notes and notes",
                ProjectId = 8,
                NewInitiativeName = $"TestInitiative_{DateTime.Now}"
            };

            var result = await _mediator.Send(command);

            Assert.True(result.IsSuccess, $"AddSimpleTskToNewInitiativeInExistingProject failed: {result}");

            Assert.True(result.HasSuccess<EntitySuccess<Tsk>>(t => t.Action == ResultAction.Add));
            Assert.True(result.HasSuccess<EntitySuccess<Tsk>>(t => t.Entity.Name == command.Name));
            Assert.True(result.HasSuccess<EntitySuccess<Tsk>>(t => t.Entity.Status == command.Status));
            Assert.True(result.HasSuccess<EntitySuccess<Tsk>>(t => t.Entity.Notes == command.Notes));

            Assert.True(result.HasSuccess<EntitySuccess<Initiative>>(t => t.Action == ResultAction.Add));
            Assert.True(result.HasSuccess<EntitySuccess<Initiative>>(t => t.Entity.Name == command.NewInitiativeName &&
                                                                         t.Entity.ProjectId == command.ProjectId));

            _output.WriteLine(result.ToString());
        }

        [Fact]
        public async Task AddSimpleTskToExistingInitiative() {

            var command = new CreateTskCommand.Command {
                Name = $"SimpleTsk_{DateTime.Now}",
                Status = Status.Open,
                Notes = "Notes and notes and notes",
                InitiativeId = 64
            };

            var result = await _mediator.Send(command);

            Assert.True(result.IsSuccess, $"AddSimpleTskToExistingInitiative failed: {result}");

            Assert.True(result.HasSuccess<EntitySuccess<Tsk>>(t => t.Action == ResultAction.Add));
            Assert.True(result.HasSuccess<EntitySuccess<Tsk>>(t => t.Entity.Name == command.Name));
            Assert.True(result.HasSuccess<EntitySuccess<Tsk>>(t => t.Entity.Status == command.Status));
            Assert.True(result.HasSuccess<EntitySuccess<Tsk>>(t => t.Entity.Notes == command.Notes));

            Assert.True(result.HasSuccess<EntitySuccess<Initiative>>(t => t.Action == ResultAction.Find));
            Assert.True(result.HasSuccess<EntitySuccess<Initiative>>(t => t.Entity.Id == command.InitiativeId));

            _output.WriteLine(result.ToString());
        }
    }
}
