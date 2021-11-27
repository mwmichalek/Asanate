using Mwm.MyQ.Application.Services;
using Mwm.MyQ.Application.Tsks.Commands;
using Mwm.MyQ.Application.Utils;
using Mwm.MyQ.Domain;
using Mwm.MyQ.Persistance.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Mwm.MyQ.Service.Tests.EntityService {
    public class TskServiceTests {

        private readonly IDatabaseContext _databaseContext;
        private readonly ITestOutputHelper _output;
        private readonly IEntityService<Tsk> _entityService;

        public TskServiceTests(IEntityService<Tsk> entityService, IDatabaseContext databaseContext, ITestOutputHelper output) {
            _databaseContext = databaseContext;
            _entityService = entityService;
            _output = output;
        }

        [Fact]
        public async Task AddSimpleTskToTriage() {
            var name = $"SimpleTsk_{DateTime.Now}";
            var command = new TskAdd.Command {
                Name = name
            };

            var result = await _entityService.ExecuteAddCommand(command);
            Assert.True(result.IsSuccess, $"AddComplexTskToTriage failed: {result}");
            Assert.True(result.HasSuccess<EntitySuccess<Tsk>>(t => t.Action == ResultAction.Add));
            Assert.True(result.HasSuccess<EntitySuccess<Tsk>>(t => t.Entity.Name == command.Name));

            var tsk = _databaseContext.Tsks.Find(result.Value);
            Assert.Equal(command.Name, tsk.Name);

            _output.WriteLine(result.ToString());
        }
    }
}
