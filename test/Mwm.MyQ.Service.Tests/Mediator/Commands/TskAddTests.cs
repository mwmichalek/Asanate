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

namespace Mwm.MyQ.Service.Tests.Mediator.Commands {

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
                ExternalId = Guid.NewGuid().ToString(),
                Status = Status.Done,
                IsArchived = false,
                IsCompleted = false,
                DurationEstimate = 3,
                DurationCompleted = 1,
                Notes = "Notes and notes and notes",
                DueDate = DateTime.Today.AddDays(5),
                StartDate = DateTime.Today.AddDays(-10),
                StartedDate = DateTime.Today.AddDays(-2),
                CompletedDate = DateTime.Today.AddDays(3)
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
