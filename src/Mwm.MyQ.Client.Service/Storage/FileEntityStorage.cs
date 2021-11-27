using Mwm.MyQ.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Mwm.MyQ.Client.Service.Storage {

    //public class FileEntityStorage : IEntityStorage {
    //    public Task<int> Add<TEntity>(TEntity entity) where TEntity : INamedEntity {
    //        throw new NotImplementedException();
    //    }

    //    public Task<int> Delete<TEntity>(TEntity entity) where TEntity : INamedEntity {
    //        throw new NotImplementedException();
    //    }

    //    public async Task<List<TEntity>> GetAll<TEntity>() where TEntity : INamedEntity {

    //        var jsonPath = $"Data/{typeof(TEntity).Name}.json";
    //        Console.WriteLine($"jsonPath: {jsonPath}");

    //        var json = await File.ReadAllTextAsync(jsonPath);
    //        return JsonConvert.DeserializeObject<List<TEntity>>(json);
    //    }

    //    public Task<int> Update<TEntity>(TEntity entity) where TEntity : INamedEntity {
    //        throw new NotImplementedException();
    //    }
    //}
}
