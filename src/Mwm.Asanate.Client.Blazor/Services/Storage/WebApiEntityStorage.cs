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

        Task<int> Add<TEntity>(TEntity entity) where TEntity : INamedEntity;

        Task<int> Update<TEntity>(TEntity entity) where TEntity : INamedEntity;

        Task<int> Delete<TEntity>(TEntity entity) where TEntity : INamedEntity;

    }
    public class WebApiEntityStorage : IEntityStorage {

        private HttpClient _httpClient;

        public WebApiEntityStorage(HttpClient httpClient) => (_httpClient) = (httpClient);

        public async Task<List<TEntity>> GetAll<TEntity>() where TEntity : INamedEntity {
            return await _httpClient.GetFromJsonAsync<List<TEntity>>($"/api/{typeof(TEntity).Name}");
        }

        public async Task<int> Add<TEntity>(TEntity entity) where TEntity : INamedEntity {
            var response = await _httpClient.PostAsJsonAsync($"/api/{typeof(TEntity).Name}/Add", entity);

            if (response.IsSuccessStatusCode &&
                int.TryParse(await response.Content.ReadAsStringAsync(), out int id))
                return id;
            throw new Exception(response.ReasonPhrase);
        }

        public async Task<int> Update<TEntity>(TEntity entity) where TEntity : INamedEntity {
            var response = await _httpClient.PostAsJsonAsync($"/api/{typeof(TEntity).Name}/Update", entity);

            if (response.IsSuccessStatusCode &&
                int.TryParse(await response.Content.ReadAsStringAsync(), out int id))
                return id;
            throw new Exception(response.ReasonPhrase);
        }

        public async Task<int> Delete<TEntity>(TEntity entity) where TEntity : INamedEntity {
            var response = await _httpClient.PostAsJsonAsync($"/api/{typeof(TEntity).Name}/Delete", entity);

            if (response.IsSuccessStatusCode &&
                int.TryParse(await response.Content.ReadAsStringAsync(), out int id))
                return id;
            throw new Exception(response.ReasonPhrase);
        }



    }
}
