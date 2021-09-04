using FluentResults;
using Mwm.Asanate.Model;
using Mwm.Asanate.Service.AsanaApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Mwm.Asanate.Service.Tests.AsanaApi {
    public class AsanaServiceTests {

        private readonly ITestOutputHelper output;

        public AsanaServiceTests(ITestOutputHelper output) {
            this.output = output;
        }

        // ********************************** TSKS ***************************************************
        [Fact]
        public async Task RetrieveAllTsks() {
            var tsks = await RetrieveAllAndAssertResults<Tsk>(DateTime.Now.AddHours(-3));
            Assert.True(tsks.Count > 0);

            foreach (var tsk in tsks)
                output.WriteLine($"Tsk Gid: {tsk.Gid}, Name: {tsk.Name}, AssignedTo: {tsk.AssignedTo.Name}");
        }

        [Fact]
        public async Task PersistTsk() {
            var tskName = $"Test {DateTime.Now.Ticks}";
            var tsk = new Tsk {
                Name = tskName,
                AssignedTo = User.Me,
                Projects = Project.ToProjectArray("1200874933882307")
            };

            var responseTsk = await PersistAndAssertResults(tsk);
            Assert.Equal(tskName, responseTsk.Name);
        }

        // ********************************** PROJECTS ***********************************************
        [Fact]
        public async Task RetrieveAllProjects() {
            var projects = await RetrieveAllAndAssertResults<Project>();
            foreach (var prj in projects)
                output.WriteLine($"Project Gid: {prj.Gid}, Company: {prj.Company}, Name: {prj.Name}, Workspace.Gid: {prj.Workspace.Gid}");
            Assert.True(projects.Count > 0);
        }

        // ********************************** USERS ***************************************************
        [Fact]
        public async Task RetrieveAllUsers() {
            var users = await RetrieveAllAndAssertResults<User>();
            Assert.True(users.Count > 0);

            foreach (var user in users)
                output.WriteLine($"User Gid: {user.Gid}, Name: {user.Name}");
        }

        // ********************************** SECTIONS ***************************************************
        [Fact]
        public async Task RetrieveAllSections() {
            var sections = await RetrieveAllAndAssertResults<Section>();
            Assert.True(sections.Count > 0);

            foreach (var section in sections)
                output.WriteLine($"Section Gid: {section.Gid}, Name: {section.Name}");
        }

        //************************************************************************************************

        private async Task<List<TEntity>> RetrieveAllAndAssertResults<TEntity>(DateTime? modifiedSince = null) where TEntity : AsanaEntity {
            var asanaService = new AsanaService(AsanaHttpClientFactory.CreateClient());
            var result = await asanaService.RetrieveAll<TEntity>(modifiedSince);
            Assert.True(result.IsSuccess);
            return result.Value;
        }

        private async Task<TEntity> PersistAndAssertResults<TEntity>(TEntity entity) where TEntity : AsanaEntity {
            var asanaService = new AsanaService(AsanaHttpClientFactory.CreateClient());
            var result = await asanaService.Persist<TEntity>(entity);
            Assert.True(result.IsSuccess);
            return result.Value;
        }
    }
}
