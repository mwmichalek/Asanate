using MediatR;
using Mwm.MyQ.Application.Shared.Commands;
using Mwm.MyQ.Data;
using Mwm.MyQ.Data.Utils;
using Mwm.MyQ.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
//using Xunit.Extensions.Logging;
using Mwm.Asana.Model.Converters;
using Mwm.MyQ.Persistance.Shared;
using Mwm.MyQ.Application.Asana.Commands;

namespace Mwm.MyQ.Service.Tests.Mediator.Commands {

   
    public class AsanaEntitiesSynchTests {
        private readonly IMediator _mediator;
        private readonly IDatabaseContext _databaseContext;
        private readonly ITestOutputHelper _output;

        public AsanaEntitiesSynchTests(IMediator mediator, IDatabaseContext databaseContext, ITestOutputHelper output) {
            _databaseContext = databaseContext;
            _mediator = mediator;
            _output = output;
        }

        [Fact]
        public void CreateNewDatabase() {
            _databaseContext.RecreateDatabase();
        }

        [Fact]
        public async Task ReRunSynch() {
            _databaseContext.EnsureCreated();
            await RunSynch();
        }

        [Fact]
        public async Task CreateNewDatabaseAndRunSynch() {
            _databaseContext.RecreateDatabase();
            await RunSynch();
        }

        private async Task RunSynch() {
            var command = new AsanaEntitiesSynch.Command {
                //Since = DateTime.Now.AddDays(-10)
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

            var tsks = _databaseContext.Tsks.ToList();
            Assert.True(tsks.Count > 0);
            tsks.ForEach(t => Assert.NotNull(t.Initiative));

        }
    }
}

