using Mwm.Asanate.Application.Shared.Commands;
using Mwm.Asanate.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Mwm.Asanate.Client.Service.Storage {

    public interface IEntityStorage {

        Task<List<TEntity>> GetAll<TEntity>() where TEntity : INamedEntity;

        Task<int> Add<TEntity, TAddEntityCommand>(TAddEntityCommand entityCommand) where TEntity : INamedEntity
                                                                                   where TAddEntityCommand : IAddEntityCommand<TEntity>;

        Task<int> Update<TEntity>(TEntity entity) where TEntity : INamedEntity;

        Task<int> Delete<TEntity>(TEntity entity) where TEntity : INamedEntity;

    }


    //public class EntityController<TEntity, TAddEntityCommand, TUpdateEntityCommand, TDeleteEntityCommand> :
    //                      ControllerBase, IEntityController<TEntity>
    //                      where TEntity : NamedEntity
    //                      where TAddEntityCommand : IAddEntityCommand<TEntity>
    //                      where TUpdateEntityCommand : IUpdateEntityCommand<TEntity>
    //                      where TDeleteEntityCommand : IDeleteEntityCommand<TEntity> {



        public class WebApiEntityStorage : IEntityStorage {

        private HttpClient _httpClient;

        public WebApiEntityStorage(HttpClient httpClient) => (_httpClient) = (httpClient);

        public async Task<List<TEntity>> GetAll<TEntity>() where TEntity : INamedEntity {
            return await _httpClient.GetFromJsonAsync<List<TEntity>>($"/api/{typeof(TEntity).Name}");
        }

        public async Task<int> Add<TEntity, TAddEntityCommand>(TAddEntityCommand entityCommand) where TEntity : INamedEntity
                                                                                                where TAddEntityCommand : IAddEntityCommand<TEntity> {
            var response = await _httpClient.PostAsJsonAsync($"/api/{typeof(TEntity).Name}/Add", entityCommand);

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
