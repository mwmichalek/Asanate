using FluentResults;
using Mwm.Asana.Model;
using Mwm.Asana.Model.Converters;
using Mwm.Asana.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Mwm.Asanate.Service.Tests {
    public class AsanaServiceTests {

        private readonly ITestOutputHelper output;

        public AsanaServiceTests(ITestOutputHelper output) {
            this.output = output;
        }

        // ********************************** TSKS ***************************************************
        [Fact]
        public async Task RetrieveAllTsks() {
            var tsks = await RetrieveAllAndAssertResults<AsanaTsk>(DateTime.Now.AddHours(-3));
            Assert.True(tsks.Count > 0);

            foreach (var tsk in tsks)
                output.WriteLine($"Tsk Gid: {tsk.Gid}, Name: {tsk.Name}, Status: {tsk.Status}, AssignedTo: {tsk.AssignedTo.Name}");
        }

        [Fact]
        public async Task PersistTsk() {
            var tskName = $"Test {DateTime.Now.Hour}:{DateTime.Now.Minute}";
            var tsk = new AsanaTsk {
                Name = tskName,
                AssignedTo = AsanaUser.Me,
                Notes = "This rocks!" + Environment.NewLine +
                        "Right?",
                DueAt = DateTime.UtcNow.AddHours(5).ToUtcIsoDate(),
                Memberships = new AsanaMembership[] {
                    new AsanaMembership {
                        Section = new AsanaSection { Gid = "1200874933882312" },
                        Project = new AsanaProject { Gid = "1200874933882307"}
                    }
                }
                //Projects = Project.ToProjectArray("1200874933882307")
            };

            var responseTsk = await PersistAndAssertResults(tsk);
            Assert.Equal(tskName, responseTsk.Name);
        }

        // ********************************** PROJECTS ***********************************************
        [Fact]
        public async Task RetrieveAllProjects() {
            var projects = await RetrieveAllAndAssertResults<AsanaProject>();
            foreach (var prj in projects)
                output.WriteLine($"Project Gid: {prj.Gid}, Company: {prj.Company}, Name: {prj.Name}, Workspace.Gid: {prj.Workspace.Gid}");
            Assert.True(projects.Count > 0);
        }

        // ********************************** USERS ***************************************************
        [Fact]
        public async Task RetrieveAllUsers() {
            var users = await RetrieveAllAndAssertResults<AsanaUser>();
            Assert.True(users.Count > 0);

            foreach (var user in users)
                output.WriteLine($"User Gid: {user.Gid}, Name: {user.Name}");
        }

        // ********************************** SECTIONS ***************************************************
        [Fact]
        public async Task RetrieveAllSections() {
            var sections = await RetrieveAllAndAssertResults<AsanaSection>();
            Assert.True(sections.Count > 0);

            foreach (var section in sections)
                output.WriteLine($"Section Gid: {section.Gid}, Name: {section.Name}");
        }

        //************************************************************************************************

        private async Task<List<TEntity>> RetrieveAllAndAssertResults<TEntity>(DateTime? modifiedSince = null) where TEntity : AsanaEntity {
            var httpClientFactory = new AsanaHttpClientFactory();
            var asanaService = new AsanaService<TEntity>(httpClientFactory);
            var result = await asanaService.RetrieveAll(modifiedSince);
            Assert.True(result.IsSuccess);
            return result.Value;
        }

        private async Task<TEntity> PersistAndAssertResults<TEntity>(TEntity entity) where TEntity : AsanaEntity {
            var httpClientFactory = new AsanaHttpClientFactory(); 
            var asanaService = new AsanaService<TEntity>(httpClientFactory);
            var result = await asanaService.Persist(entity);
            Assert.True(result.IsSuccess);
            return result.Value;
        }
    }
}
