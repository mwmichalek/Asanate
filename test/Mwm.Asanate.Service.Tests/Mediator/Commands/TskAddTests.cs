using MediatR;
using Microsoft.EntityFrameworkCore;
using Mwm.Asanate.Application.Tsks.Commands;
using Mwm.Asanate.Application.Utils;
using Mwm.Asanate.Data;
using Mwm.Asanate.Domain;
using Mwm.Asanate.Persistance.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Mwm.Asanate.Service.Tests.Mediator.Commands {

    [Collection("Generic")]
    public class TskAddTests {
        private readonly IMediator _mediator;
        private readonly IDatabaseContext _databaseContext;
        private readonly ITestOutputHelper _output;

        public TskAddTests(IMediator mediator, IDatabaseContext databaseContext, ITestOutputHelper output) {
            _databaseContext = databaseContext;
            _mediator = mediator;
            _output = output;
        }

        [Fact]
        public async Task AddSimpleTskToTriage() {
            var name = $"SimpleTsk_{DateTime.Now}";
            var command = new TskAdd.Command {
                Name = name
            };

            var result = await _mediator.Send(command);
            Assert.True(result.IsSuccess, $"AddComplexTskToTriage failed: {result}");
            Assert.True(result.HasSuccess<EntitySuccess<Tsk>>(t => t.Action == ResultAction.Add));
            Assert.True(result.HasSuccess<EntitySuccess<Tsk>>(t => t.Entity.Name == command.Name));

            var tsk = _databaseContext.Tsks.Find(result.Value);
            Assert.Equal(command.Name, tsk.Name);

            _output.WriteLine(result.ToString());
        }

        [Fact]
        public async Task AddSimpleTskToTriageWithoutName() {
            var name = $"SimpleTsk_{DateTime.Now}";
            var command = new TskAdd.Command {
            };

            var result = await _mediator.Send(command);
            Assert.True(result.IsFailed, $"AddSimpleTskToTriageWithoutName succeeded, loser: {result}");
            Assert.True(result.HasError(e => e.Message == "Tsk Name can't be null."));

            _output.WriteLine(result.ToString());
        }

        [Fact]
        public async Task AddComplexTskToTriage() {

            var command = new TskAdd.Command {
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

            var tsk = _databaseContext.Tsks.Find(result.Value);
            Assert.Equal(command.Name, tsk.Name);

            _output.WriteLine(result.ToString());
        }

        [Fact]
        public async Task AddSimpleTskToExistingInitiative() {

            var command = new TskAdd.Command {
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

            //Assert.True(result.HasSuccess<EntitySuccess<Initiative>>(t => t.Action == ResultAction.Find));
            //Assert.True(result.HasSuccess<EntitySuccess<Initiative>>(t => t.Entity.Id == command.InitiativeId));

            var tsk = _databaseContext.Tsks.Find(result.Value);
            Assert.Equal(command.Name, tsk.Name);
            Assert.Equal(command.InitiativeId, tsk.Initiative.Id);

            _output.WriteLine(result.ToString());
        }
    }
}
