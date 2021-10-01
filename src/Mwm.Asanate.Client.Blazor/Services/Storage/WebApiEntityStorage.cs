using Mwm.Asanate.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Mwm.Asanate.Client.Blazor.Services.Storage {

    public interface IEntityStorage {

        Task<List<TEntity>> GetAll<TEntity>() where TEntity : INamedEntity;
    
    }
    public class WebApiEntityStorage : IEntityStorage {

        private HttpClient _httpClient;

        public WebApiEntityStorage(HttpClient httpClient) => (_httpClient) = (httpClient);

        public async Task<List<TEntity>> GetAll<TEntity>() where TEntity : INamedEntity {
            return await _httpClient.GetFromJsonAsync<List<TEntity>>($"/api/{typeof(TEntity).Name}");
        }

    }
}
