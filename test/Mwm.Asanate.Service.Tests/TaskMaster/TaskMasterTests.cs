using FluentResults;
using Microsoft.Extensions.DependencyInjection;
using Mwm.Asana.Model;
using Mwm.Asana.Model.Converters;
using Mwm.Asana.Service;
using Mwm.Asanate.Service.Tests.Fixtures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Mwm.Asanate.Service.Tests.TaskMaster {

    [Collection("ServiceProvider Collection")]
    public class TaskMasterTests {

        private readonly ITestOutputHelper output;
        private readonly IServiceProvider serviceProvider;

        public TaskMasterTests(ITestOutputHelper output, ServiceProviderFixture serviceProviderFixture) {
            this.output = output;
            this.serviceProvider = serviceProviderFixture.ServiceProvider;
            ServiceProviderFactory.Build();
        }

        [Fact]
        public async Task RetrieveAllStuff() {
            var tskService = serviceProvider.GetService<IAsanaService<AsanaTsk>>();
            var projectService = serviceProvider.GetService<IAsanaService<AsanaProject>>();
            var sectionService = serviceProvider.GetService<ISectionAsanaService>();

            var tsksResult = await tskService.RetrieveAll();
            if (tsksResult.IsSuccess)
                output.WriteLine($"Tasks: {tsksResult.Value.Count}");

            var projectsResult = await projectService.RetrieveAll();
            if (projectsResult.IsSuccess)
                output.WriteLine($"Projects: {projectsResult.Value.Count}");

            var sectionsResult = await sectionService.RetrieveAll();
            if (sectionsResult.IsSuccess)
                output.WriteLine($"Sections: {sectionsResult.Value.Count}");

        }

    }
}
