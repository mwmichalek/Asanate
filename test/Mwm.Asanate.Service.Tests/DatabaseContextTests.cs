using Mwm.Asanate.Domain;
using Mwm.Asanate.Data.Utils;
using System;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using Mwm.Asanate.Data;

namespace Mwm.Asanate.Service.Tests {

    [Collection("Generic")]
    public class DatabaseContextTests {

        private readonly DatabaseContext _databaseContext;
        private readonly ITestOutputHelper _output;

        public DatabaseContextTests(DatabaseContext databaseContext, ITestOutputHelper output) {
            _databaseContext = databaseContext;
            _output = output;

            _databaseContext.RecreateDatabase();
        }

        [Fact]
        public void AddUser() {
            uint id = 9999;

            var me = _databaseContext.Add(new User {
                Id = id,
                Name = "Michalek"
            }); 

            _databaseContext.SaveChanges();

            var users = _databaseContext.Users.ToList();

            Assert.Equal(id, users.First().Id);
        }

        [Fact]
        public void AddTask() {
            var tsk = new Tsk {
                Name = "Sample Tsk",
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

            _databaseContext.Add(tsk);
            _databaseContext.SaveChanges();

            Assert.NotEqual(initialId, tsk.Id);

        }
    }
}
