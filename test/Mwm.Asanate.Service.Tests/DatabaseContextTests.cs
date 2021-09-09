using Mwm.Asanate.Domain;
using Mwm.Asanate.Data.Utils;
using System;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using Mwm.Asanate.Data;

namespace Mwm.Asanate.Service.Tests {

    [Collection("DatabaseContextTests")]
    public class DatabaseContextTests {

        private readonly DatabaseContext _databaseContext;
        private readonly ITestOutputHelper _output;

        public DatabaseContextTests(DatabaseContext databaseContext, ITestOutputHelper output) {
            _databaseContext = databaseContext;
            _output = output;

            _databaseContext.RecreateDatabase();
        }

        [Fact]
        public void Test1() {
            var id = Guid.NewGuid().ToString();

            var me = _databaseContext.Add(new User {
                Id = id,
                Name = "Michalek"
            }); 

            _databaseContext.SaveChanges();

            var users = _databaseContext.Users.ToList();

            Assert.Equal(id, users.First().Id);
        }
    }
}
