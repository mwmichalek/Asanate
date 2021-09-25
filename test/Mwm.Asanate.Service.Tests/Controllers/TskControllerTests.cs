using MediatR;
using Microsoft.Extensions.Hosting;
using Mwm.Asanate.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Mwm.Asanate.Service.Tests.Controllers {

    [Collection("Generic")]
    public class TskControllerTests {

        private readonly IMediator _mediator;
        private readonly DatabaseContext _databaseContext;
        private readonly ITestOutputHelper _output;

        public TskControllerTests(DatabaseContext databaseContext, ITestOutputHelper output) {
            _databaseContext = databaseContext;
            _output = output;

            
        }

        [Fact]
        public async Task CreateSimpleTsk() {
            //Task.Run(() => Mwm.Asanate.Server.Program.CreateHostBuilder().Build().Run());

            //var httpClient = new Htt

        }
    }
}
