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
            //Assert.True(response.Count > 0);
        }

        [Fact]
        public async Task Add() {

            var tskCommand = new TskAdd.Command {
                Name = "This is a test",
                Status = Status.Open
            };

            var id = await _entityStorage.Add<Tsk, TskAdd.Command>(tskCommand);

            Assert.True(id > 0);


            //var tsk = new TskAdd.Command {
            //    Name = "This is a test",
            //    Status = Status.Open
            //};
            //var response = await _httpClient.PostAsJsonAsync("/api/Tsk/Add", tsk);

            //Assert.True(response.IsSuccessStatusCode);
            //var result = await response.Content.ReadAsStringAsync();
            //_output.WriteLine(result);
            //Assert.True(int.TryParse(result, out int intResult));
            //Assert.True(intResult > 0);
        }

        [Fact]
        public async Task Update() {
            //var tsk = new TskUpdate.Command {
            //    Name = "This is a test againmmmmm",
            //    Status = Status.Done,
            //    IsCompleted = true,
            //    Id = 213
            //};
            //var response = await _httpClient.PostAsJsonAsync("/api/Tsk/Update", tsk);

            //Assert.True(response.IsSuccessStatusCode);
            //var result = await response.Content.ReadAsStringAsync();
            //_output.WriteLine(result);
            //Assert.True(int.TryParse(result, out int intResult));
            //Assert.Equal(tsk.Id, intResult);
        }

        [Fact]
        public async Task Delete() {
            //var tsk = new TskDelete.Command {
            //    Id = 10
            //};
            //var response = await _httpClient.PostAsJsonAsync("/api/Tsk/Delete", tsk);

            //Assert.True(response.IsSuccessStatusCode);
            //var result = await response.Content.ReadAsStringAsync();
            //_output.WriteLine(result);
            //Assert.True(int.TryParse(result, out int intResult));
            //Assert.Equal(tsk.Id, intResult);
        }
    }
}
