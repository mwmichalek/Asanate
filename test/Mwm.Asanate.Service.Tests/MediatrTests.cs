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
            //_databaseContext.Database.EnsureCreated();
        }

        [Fact]
        public async Task SynchronizeAsanaEntities() {
            var command = new SynchronizeAsanaEntitiesCommand.Command {
                Since = DateTime.Now.AddDays(-10)
            };

            var result = await _mediator.Send(command);
            Assert.True(result.IsSuccess, $"SynchronizeAsanaEntitiesCommand failed: {result}");

            var projects = _databaseContext.Projects.ToList();
            Assert.True(projects.Count > 0);
            projects.ForEach(p => Assert.NotNull(p.Company));

            var companies = _databaseContext.Companies.ToList();
            Assert.True(companies.Count > 0);
            companies.ForEach(c => Assert.True(c.Projects.Count > 0));

            var initiatives = _databaseContext.Initiatives.ToList();
            Assert.True(initiatives.Count > 0);
            initiatives.ForEach(i => Assert.True(i.Tsks.Count > 0));

            var tsks = _databaseContext.Tsks.ToList();
            Assert.True(tsks.Count > 0);
            tsks.ForEach(t => Assert.NotNull(t.Initiative));

        }
    }
}

