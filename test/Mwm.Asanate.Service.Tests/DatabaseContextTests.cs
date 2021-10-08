using Mwm.Asanate.Domain;
using Mwm.Asanate.Data.Utils;
using System;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using Mwm.Asanate.Data;
using Mwm.Asanate.Persistance.Shared;

namespace Mwm.Asanate.Service.Tests {

    [Collection("Generic")]
    public class DatabaseContextTests {

        private readonly IDatabaseContext _databaseContext;
        private readonly ITestOutputHelper _output;

        public DatabaseContextTests(IDatabaseContext databaseContext, ITestOutputHelper output) {
            _databaseContext = databaseContext;
            _output = output;

            _databaseContext.RecreateDatabase();
        }

        [Fact]
        public void AddUser() {
            var me = _databaseContext.Users.Add(new User {
                Id = 0,
                Name = "Some Dude"
            });

            _databaseContext.Save();

            var users = _databaseContext.Users.ToList();

            Assert.NotEqual(0, users.First().Id);
        }

        [Fact]
        public void AddTask() {
            var tsk = new Tsk {
                Name = "Sample Tsk",
                AssignedTo = new User {  Name = "TheMan"},
                Initiative = new Initiative {
                    Name = "Sample Initiative",
                    Project = new Project {
                        Name = "Sample Project",
                        Company = new Company {
                            Name = "Sample Company"
                        }
                    }
                }
            };
            var initialId = tsk.Id;

            _databaseContext.Tsks.Add(tsk);
            _databaseContext.Save();

            Assert.NotEqual(initialId, tsk.Id);

        }
    }
}
