using Mwm.Asana.Model;
using Mwm.Asana.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Mwm.MyQ.Service.Tests {
    public class MemoryCacheAsanaServiceTests {

        private readonly ITestOutputHelper output;

        public MemoryCacheAsanaServiceTests(ITestOutputHelper output) {
            this.output = output;
        }

        // ********************************** TSKS ***************************************************
        [Fact(Skip = "On longer in use.")]
        public async Task RetrieveAllTsks() {
            var tsks = await RetrieveAllAndAssertResults<AsanaTsk>();
            Assert.True(tsks.Count > 0);

            foreach (var tsk in tsks)
                output.WriteLine($"Tsk Gid: {tsk.Gid}, Name: {tsk.Name}, Status: {tsk.Status}, AssignedTo: {tsk.AssignedTo.Name}");
        }

        //************************************************************************************************

        private async Task<List<TEntity>> RetrieveAllAndAssertResults<TEntity>(DateTime? modifiedSince = null) where TEntity : IAsanaEntity {
            var httpClientFactory = new AsanaHttpClientFactory(); 
            var asanaService = new MemoryCacheAsanaService<TEntity>(httpClientFactory);
            var result = await asanaService.RetrieveAll(modifiedSince);
            Assert.True(result.IsSuccess);
            return result.Value;
        }

        private async Task<TEntity> PersistAndAssertResults<TEntity>(TEntity entity) where TEntity : IAsanaEntity {
            var httpClientFactory = new AsanaHttpClientFactory(); 
            var asanaService = new MemoryCacheAsanaService<TEntity>(httpClientFactory);
            var result = await asanaService.Persist(entity);
            Assert.True(result.IsSuccess);
            return result.Value;
        }
    }
}
