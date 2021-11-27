using MediatR;
using Microsoft.EntityFrameworkCore;
using Mwm.MyQ.Application.Projects.Commands;
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
    public class ProjectUpdateTests {
        private readonly IMediator _mediator;
        private readonly IDatabaseContext _databaseContext;
        private readonly ITestOutputHelper _output;

        public ProjectUpdateTests(IMediator mediator, IDatabaseContext databaseContext, ITestOutputHelper output) {
            _databaseContext = databaseContext;
            _mediator = mediator;
            _output = output;
        }

        [Fact]
        public async Task UpdateSimpleProject() {
            var name = $"SimpleProject_{DateTime.Now}";

            var companyId = _databaseContext.Companies.Where(p => p.Name == "MWM")
                                                      .Select(p => p.Id).FirstOrDefault();    
            var command = new ProjectUpdate.Command {
                Id = 1,
                Name = name,
                CompanyId = companyId
            };
            var result = await _mediator.Send(command);
            Assert.True(result.IsSuccess, $"Add Project failed: {result}");
            Assert.True(result.HasSuccess<EntitySuccess<Project>>(t => t.Action == ResultAction.Update));
            Assert.True(result.HasSuccess<EntitySuccess<Project>>(t => t.Entity.Name == command.Name));

            var project = _databaseContext.Projects.Find(result.Value);
            Assert.Equal(command.Name, project.Name);

            _output.WriteLine(result.ToString());
        }
    }
}
