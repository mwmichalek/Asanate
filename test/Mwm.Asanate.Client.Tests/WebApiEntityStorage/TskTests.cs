using Mwm.Asanate.Client.Service.Storage;
using Mwm.Asanate.Domain;
using Mwm.Asanate.Persistance.Shared;
using Mwm.Asanate.Application.Tsks.Commands;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Mwm.Asanate.Client.Tests.WebApiEntityService {

    [Collection("WebApi")]

    public class TskTests {
        private readonly IDatabaseContext _databaseContext;
        private readonly ITestOutputHelper _output;
        //private readonly HttpClient _httpClient;
        private readonly IEntityStorage _entityStorage;

        public TskTests(IDatabaseContext databaseContext, ITestOutputHelper output, IEntityStorage entityStorage) { // , HttpClient httpClient
            _databaseContext = databaseContext;
            _output = output;
            //_httpClient = httpClient;
            _entityStorage = entityStorage;
        }

        [Fact]
        public async Task All() {
            var tsks = await _entityStorage.GetAll<Tsk>();
            Assert.NotNull(tsks);
            Assert.True(tsks.Count > 0);
        }

        [Fact]
        public async Task Get() {
            var lastId = _databaseContext.Set<Tsk>().OrderByDescending(t => t.Id).Select(t => t.Id).FirstOrDefault();
            
            var tsk = await _entityStorage.Get<Tsk>(lastId);
            Assert.NotNull(tsk);
            Assert.Equal(lastId, tsk.Id);
        }

        [Fact]
        public async Task Add() {
            var tskCommand = new TskAdd.Command {
                Name = "This is a test",
                Status = Status.Open
            };

            var id = await _entityStorage.Add<Tsk, TskAdd.Command>(tskCommand);
            Assert.True(id > 0);

            var tsk = _databaseContext.Set<Tsk>().SingleOrDefault(t => t.Id == id);
            Assert.Equal(tskCommand.Name, tsk.Name);
            Assert.Equal(tskCommand.Status, tsk.Status);
        }

        [Fact]
        public async Task Update() {
            var lastId = _databaseContext.Set<Tsk>().OrderByDescending(t => t.Id).Select(t => t.Id).FirstOrDefault();

            var tskCommand = new TskUpdate.Command {
                Name = "This is a test againmmmmm",
                Status = Status.Done,
                IsCompleted = true,
                Id = lastId
            };

            var id = await _entityStorage.Update<Tsk, TskUpdate.Command>(tskCommand);
            var tsk = _databaseContext.Set<Tsk>().SingleOrDefault(t => t.Id == id);
            Assert.Equal(tskCommand.Name, tsk.Name);
            Assert.Equal(tskCommand.Status, tsk.Status);
        }

        [Fact]
        public async Task Delete() {
            var lastId = _databaseContext.Set<Tsk>().OrderByDescending(t => t.Id).Select(t => t.Id).FirstOrDefault();

            var tskCommand = new TskDelete.Command {
                Id = lastId
            };

            var id = await _entityStorage.Delete<Tsk, TskDelete.Command>(tskCommand);
            var tsk = _databaseContext.Set<Tsk>().SingleOrDefault(t => t.Id == id);
            Assert.Null(tsk);
        }
    }
}
