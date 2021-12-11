using Mwm.MyQ.Application.Shared.Commands;
using Mwm.MyQ.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Mwm.MyQ.Client.Service.Storage {

    public interface IEntityStorage {

        Task<List<TEntity>> GetAll<TEntity>() where TEntity : INamedEntity;

        Task<TEntity> Get<TEntity>(int id) where TEntity : INamedEntity;

        Task<int> Add<TEntity, TAddEntityCommand>(TAddEntityCommand entityCommand) where TEntity : INamedEntity
                                                                                   where TAddEntityCommand : IAddEntityCommand<TEntity>;

        Task<int> Update<TEntity, TUpdateEntityCommand>(TUpdateEntityCommand entityCommand) where TEntity : INamedEntity
                                                                                            where TUpdateEntityCommand : IUpdateEntityCommand<TEntity>;

        Task<int> Delete<TEntity, TDeleteEntityCommand>(TDeleteEntityCommand entityCommand) where TEntity : INamedEntity
                                                                                            where TDeleteEntityCommand : IDeleteEntityCommand<TEntity>;


    }

    public class WebApiEntityStorage : IEntityStorage {

        private HttpClient _httpClient;

        public WebApiEntityStorage(HttpClient httpClient) => (_httpClient) = (httpClient);

        public async Task<List<TEntity>> GetAll<TEntity>() where TEntity : INamedEntity {
            return await _httpClient.GetFromJsonAsync<List<TEntity>>($"/api/{typeof(TEntity).Name}");
        }

        public async Task<TEntity> Get<TEntity>(int id) where TEntity : INamedEntity {
            return await _httpClient.GetFromJsonAsync<TEntity>($"/api/{typeof(TEntity).Name}/{id}");
        }

        public async Task<int> Add<TEntity, TAddEntityCommand>(TAddEntityCommand entityCommand) where TEntity : INamedEntity
                                                                                                where TAddEntityCommand : IAddEntityCommand<TEntity> {
            var response = await _httpClient.PostAsJsonAsync($"/api/{typeof(TEntity).Name}/Add", entityCommand);

            if (response.IsSuccessStatusCode &&
                int.TryParse(await response.Content.ReadAsStringAsync(), out int id))
                return id;
            var errorMsg = await response.Content.ReadAsStringAsync();
            throw new Exception(errorMsg);
        }

        public async Task<int> Update<TEntity, TUpdateEntityCommand>(TUpdateEntityCommand entityCommand) where TEntity : INamedEntity
                                                                                                         where TUpdateEntityCommand : IUpdateEntityCommand<TEntity> {
            var response = await _httpClient.PostAsJsonAsync($"/api/{typeof(TEntity).Name}/Update", entityCommand);

            if (response.IsSuccessStatusCode &&
                int.TryParse(await response.Content.ReadAsStringAsync(), out int id))
                return id;
            var errorMsg = await response.Content.ReadAsStringAsync();
            throw new Exception(errorMsg);
        }

        public async Task<int> Delete<TEntity, TDeleteEntityCommand>(TDeleteEntityCommand entityCommand) where TEntity : INamedEntity
                                                                                                         where TDeleteEntityCommand : IDeleteEntityCommand<TEntity> {
            var response = await _httpClient.PostAsJsonAsync($"/api/{typeof(TEntity).Name}/Delete", entityCommand);

            if (response.IsSuccessStatusCode &&
                int.TryParse(await response.Content.ReadAsStringAsync(), out int id))
                return id;
            var errorMsg = await response.Content.ReadAsStringAsync();
            throw new Exception(errorMsg);
        }

    }
}
