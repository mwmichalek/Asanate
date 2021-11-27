using MediatR;
using Mwm.MyQ.Application.Shared.Commands;
using Mwm.MyQ.Data;
using Mwm.MyQ.Data.Utils;
using Mwm.MyQ.Domain;
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
using Mwm.MyQ.Persistance.Shared;

namespace Mwm.MyQ.Service.Tests.Utils {

    public class JsonExportUtils {
        private readonly IDatabaseContext _databaseContext;
        private readonly ITestOutputHelper _output;

        public JsonExportUtils(IDatabaseContext databaseContext, ITestOutputHelper output) {
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
            File.WriteAllText($"../../../../../src/Mwm.MyQ.Client.Blazor/Data/{typeof(TEntity).Name}.json", entities.ToJson());
        }

    }
}

