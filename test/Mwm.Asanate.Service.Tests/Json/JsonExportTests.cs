using MediatR;
using Mwm.Asanate.Application.Shared.Commands;
using Mwm.Asanate.Data;
using Mwm.Asanate.Data.Utils;
using Mwm.Asanate.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using Xunit.Extensions.Logging;
using Mwm.Asana.Model.Converters;

namespace Mwm.Asanate.Service.Tests.Json {

    public class JsonExportTests {
        private readonly DatabaseContext _databaseContext;
        private readonly ITestOutputHelper _output;

        public JsonExportTests(DatabaseContext databaseContext, ITestOutputHelper output) {
            _databaseContext = databaseContext;
            _output = output;
        }

        [Fact]
        public void SaveTsksAsJsonFiles() {
            SaveEntitiesToJsonFile<Tsk>(_databaseContext.Tsks.ToList());
        }

        [Fact]
        public void SaveInitiativesAsJsonFiles() {
            SaveEntitiesToJsonFile<Initiative>(_databaseContext.Initiatives.ToList());
        }

        [Fact]
        public void SaveProjectsAsJsonFiles() {
            SaveEntitiesToJsonFile<Project>(_databaseContext.Projects.ToList());
        }

        [Fact]
        public void SaveCompaniesAsJsonFiles() {
            SaveEntitiesToJsonFile<Company>(_databaseContext.Companies.ToList());
        }

        private void SaveEntitiesToJsonFile<TEntity>(List<TEntity> entities) where TEntity : INamedEntity {
            File.WriteAllText($"../../../../../data/{typeof(TEntity).Name}.json", entities.ToJson());
        }

    }
}

