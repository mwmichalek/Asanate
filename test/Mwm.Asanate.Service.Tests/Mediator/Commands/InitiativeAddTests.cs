using MediatR;
using Microsoft.EntityFrameworkCore;
using Mwm.Asanate.Application.Initiatives.Commands;
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
    public class InitiativeAddTests {
        private readonly IMediator _mediator;
        private readonly IDatabaseContext _databaseContext;
        private readonly ITestOutputHelper _output;

        public InitiativeAddTests(IMediator mediator, IDatabaseContext databaseContext, ITestOutputHelper output) {
            _databaseContext = databaseContext;
            _mediator = mediator;
            _output = output;

        }

        [Fact]
        public async Task AddSimpleInitiative() {
            var name = $"SimpleInitiative_{DateTime.Now}";
            var command = new InitiativeAdd.Command {
                Name = name,
                ProjectId = 1
            };

            var result = await _mediator.Send(command);
            Assert.True(result.IsSuccess, $"AddSimpleInitiative failed: {result}");
            Assert.True(result.HasSuccess<EntitySuccess<Initiative>>(t => t.Action == ResultAction.Add));
            Assert.True(result.HasSuccess<EntitySuccess<Initiative>>(t => t.Entity.Name == command.Name));

            var initiative = _databaseContext.Initiatives.Find(result.Value);
            Assert.Equal(command.Name, initiative.Name);
            Assert.Equal(command.ProjectId, initiative.ProjectId);

            _output.WriteLine(result.ToString());
        }

        [Fact]
        public async Task AddSimpleInitiativeWithoutProjectId() {
            var name = $"SimpleInitiative_{DateTime.Now}";
            var command = new InitiativeAdd.Command {
                Name = name
            };

            var result = await _mediator.Send(command);
            Assert.True(result.IsFailed, $"AddSimpleInitiative succeeded, loser: {result}");
            Assert.True(result.HasError(e => e.Message == "Initiative ProjectId must be set."));

            _output.WriteLine(result.ToString());
        }

        [Fact]
        public async Task AddSimpleInitiativeWithoutName() {
            var name = $"SimpleInitiative_{DateTime.Now}";
            var command = new InitiativeAdd.Command {
                ProjectId = 1
            };

            var result = await _mediator.Send(command);
            Assert.True(result.IsFailed, $"AddSimpleInitiative succeeded, loser: {result}");
            Assert.True(result.HasError(e => e.Message == "Initiative Name can't be null."));

            _output.WriteLine(result.ToString());
        }




        //[Fact]
        //public async Task AddComplexTskToTriage() {

        //    var command = new TskAdd.Command {
        //        Name = $"ComplexTsk_{DateTime.Now}",
        //        Status = Status.Done,
        //        Notes = "Notes and notes and notes",
        //        StartedDate = DateTime.Today.AddDays(-2),
        //        DueDate = DateTime.Today.AddDays(5),
        //        CompletedDate = DateTime.Today.AddDays(3),
        //        IsArchived = true,
        //    };

        //    var result = await _mediator.Send(command);

        //    Assert.True(result.IsSuccess, $"AddComplexTskToTriage failed: {result}");

        //    Assert.True(result.HasSuccess<EntitySuccess<Tsk>>(t => t.Action == ResultAction.Add));
        //    Assert.True(result.HasSuccess<EntitySuccess<Tsk>>(t => t.Entity.Name == command.Name));
        //    Assert.True(result.HasSuccess<EntitySuccess<Tsk>>(t => t.Entity.Status == command.Status));
        //    Assert.True(result.HasSuccess<EntitySuccess<Tsk>>(t => t.Entity.Notes == command.Notes));
        //    Assert.True(result.HasSuccess<EntitySuccess<Tsk>>(t => t.Entity.DueDate == command.DueDate));
        //    Assert.True(result.HasSuccess<EntitySuccess<Tsk>>(t => t.Entity.StartedDate == command.StartedDate));
        //    Assert.True(result.HasSuccess<EntitySuccess<Tsk>>(t => t.Entity.CompletedDate == command.CompletedDate));
        //    Assert.True(result.HasSuccess<EntitySuccess<Tsk>>(t => t.Entity.IsArchived == command.IsArchived));

        //    //Assert.True(result.HasSuccess<EntitySuccess<Initiative>>(t => t.Action == ResultAction.Find));
        //    //Assert.True(result.HasSuccess<EntitySuccess<Initiative>>(t => t.Entity.Name == Initiative.DefaultInitiativeName &&
        //    //                                                              t.Entity.Project.Name == Project.DefaultProjectName));

        //    var tsk = _databaseContext.Tsks.Find(result.GetSuccess<Tsk>().Entity.Id);
        //    Assert.Equal(command.Name, tsk.Name);

        //    _output.WriteLine(result.ToString());
        //}

        //[Fact(Skip ="No longer creating Initiative in this manor")]
        //public async Task AddSimpleTskToNewInitiativeInExistingProject() {

        //    var command = new TskAdd.Command {
        //        Name = $"SimpleTsk_{DateTime.Now}",
        //        Status = Status.Open,
        //        Notes = "Notes and notes and notes",
        //        //ProjectId = 8,
        //        //NewInitiativeName = $"TestInitiative_{DateTime.Now}"
        //    };

        //    var result = await _mediator.Send(command);

        //    Assert.True(result.IsSuccess, $"AddSimpleTskToNewInitiativeInExistingProject failed: {result}");

        //    Assert.True(result.HasSuccess<EntitySuccess<Tsk>>(t => t.Action == ResultAction.Add));
        //    Assert.True(result.HasSuccess<EntitySuccess<Tsk>>(t => t.Entity.Name == command.Name));
        //    Assert.True(result.HasSuccess<EntitySuccess<Tsk>>(t => t.Entity.Status == command.Status));
        //    Assert.True(result.HasSuccess<EntitySuccess<Tsk>>(t => t.Entity.Notes == command.Notes));

        //    //Assert.True(result.HasSuccess<EntitySuccess<Initiative>>(t => t.Action == ResultAction.Add));
        //    //Assert.True(result.HasSuccess<EntitySuccess<Initiative>>(t => t.Entity.Name == command.NewInitiativeName &&
        //    //                                                              t.Entity.ProjectId == command.ProjectId));

        //    var tsk = _databaseContext.Tsks.Include(t => t.Initiative.Project).SingleOrDefault(t => t.Id == result.GetSuccess<Tsk>().Entity.Id);
        //    Assert.Equal(command.Name, tsk.Name);
        //    //Assert.Equal(command.NewInitiativeName, tsk.Initiative.Name);
        //    //Assert.Equal(command.ProjectId, tsk.Initiative.ProjectId);
        //    //Assert.Equal(command.ProjectId, tsk.Initiative.Project.Id);

        //    _output.WriteLine(result.ToString());
        //}

        //[Fact]
        //public async Task AddSimpleTskToExistingInitiative() {

        //    var command = new TskAdd.Command {
        //        Name = $"SimpleTsk_{DateTime.Now}",
        //        Status = Status.Open,
        //        Notes = "Notes and notes and notes",
        //        InitiativeId = 64
        //    };

        //    var result = await _mediator.Send(command);

        //    Assert.True(result.IsSuccess, $"AddSimpleTskToExistingInitiative failed: {result}");

        //    Assert.True(result.HasSuccess<EntitySuccess<Tsk>>(t => t.Action == ResultAction.Add));
        //    Assert.True(result.HasSuccess<EntitySuccess<Tsk>>(t => t.Entity.Name == command.Name));
        //    Assert.True(result.HasSuccess<EntitySuccess<Tsk>>(t => t.Entity.Status == command.Status));
        //    Assert.True(result.HasSuccess<EntitySuccess<Tsk>>(t => t.Entity.Notes == command.Notes));

        //    Assert.True(result.HasSuccess<EntitySuccess<Initiative>>(t => t.Action == ResultAction.Find));
        //    Assert.True(result.HasSuccess<EntitySuccess<Initiative>>(t => t.Entity.Id == command.InitiativeId));

        //    var tsk = _databaseContext.Tsks.Find(result.GetSuccess<Tsk>().Entity.Id);
        //    Assert.Equal(command.Name, tsk.Name);
        //    Assert.Equal(command.InitiativeId, tsk.Initiative.Id);

        //    _output.WriteLine(result.ToString());
        //}
    }
}
