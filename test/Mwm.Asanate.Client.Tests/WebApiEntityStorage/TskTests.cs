using Mwm.Asanate.Persistance.Shared;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Mwm.Asanate.Client.Tests.WebApiEntityService {
    public class TskTests {
        private readonly IDatabaseContext _databaseContext;
        private readonly ITestOutputHelper _output;
        private readonly HttpClient _httpClient;

        public TskTests(IDatabaseContext databaseContext, ITestOutputHelper output, HttpClient httpClient) {
            _databaseContext = databaseContext;
            _output = output;
            _httpClient = httpClient;

        }

        [Fact]
        public async Task All() {
            //var response = await _httpClient.GetFromJsonAsync<List<Tsk>>("/api/Tsk");

            //Assert.NotNull(response);
            //Assert.True(response.Count > 0);
        }

        [Fact]
        public async Task Get() {
            //var response = await _httpClient.GetFromJsonAsync<Tsk>("/api/Tsk/Get/1");

            //Assert.NotNull(response);
        }

        [Fact]
        public async Task Add() {
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
