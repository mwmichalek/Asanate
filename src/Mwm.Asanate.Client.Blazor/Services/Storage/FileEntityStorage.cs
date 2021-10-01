using Mwm.Asanate.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Mwm.Asanate.Client.Blazor.Services.Storage {

    public class FileEntityStorage : IEntityStorage {

        public async Task<List<TEntity>> GetAll<TEntity>() where TEntity : INamedEntity {

            var jsonPath = $"Data/{typeof(TEntity).Name}.json";
            Console.WriteLine($"jsonPath: {jsonPath}");

            var json = await File.ReadAllTextAsync(jsonPath);
            return JsonConvert.DeserializeObject<List<TEntity>>(json);
        }

    }
}
