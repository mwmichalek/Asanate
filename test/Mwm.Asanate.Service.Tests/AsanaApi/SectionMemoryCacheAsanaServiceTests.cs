using Mwm.Asana.Model;
using Mwm.Asana.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Mwm.Asanate.Service.Tests.AsanaApi {
    public class SectionMemoryCacheAsanaServiceTests {

        private readonly ITestOutputHelper output;

        public SectionMemoryCacheAsanaServiceTests(ITestOutputHelper output) {
            this.output = output;
        }

        // ********************************** TSKS ***************************************************
        [Fact]
        public async Task RetrieveAllSections() {
            var sections = await RetrieveAllAndAssertResults();
            Assert.True(sections.Count > 0);

            foreach (var section in sections)
                output.WriteLine($"Section Gid: {section.Gid}, Name: {section.Name}");
        }

        //************************************************************************************************

        private async Task<List<AsanaSection>> RetrieveAllAndAssertResults(DateTime? modifiedSince = null) {
            var httpClientFactory = new AsanaHttpClientFactory();
            var projectService = new MemoryCacheAsanaService<AsanaProject>(httpClientFactory);
            var sectionService = new SectionMemoryCacheAsanaService(httpClientFactory, projectService);
            var result = await sectionService.RetrieveAll(modifiedSince);
            Assert.True(result.IsSuccess);
            return result.Value;
        }

        private async Task<AsanaSection> PersistAndAssertResults(AsanaSection entity) {
            var httpClientFactory = new AsanaHttpClientFactory();
            var projectService = new MemoryCacheAsanaService<AsanaProject>(httpClientFactory);
            var sectionService = new SectionMemoryCacheAsanaService(httpClientFactory, projectService);
            var result = await sectionService.Persist(entity);
            Assert.True(result.IsSuccess);
            return result.Value;
        }
    }
}
