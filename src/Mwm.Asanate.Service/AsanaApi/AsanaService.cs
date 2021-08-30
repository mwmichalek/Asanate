using Mwm.Asanate.Model;
using Mwm.Asanate.Model.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Mwm.Asanate.Service.AsanaApi {
    public class AsanaService : IAsanaService {

        private HttpClient httpClient;

        public AsanaService(HttpClient httpClient) {
            this.httpClient = httpClient;
        }

        public async Task<List<TEntity>> GetAll<TEntity>() where TEntity : AsanaEntity {
            var requestUrl = typeof(TEntity).GetUrl();
            var resultList = new List<TEntity>();
            while (requestUrl != null) {
                var json = await httpClient.GetStringAsync(requestUrl);
                var results = JsonConvert.DeserializeObject<AsanaResult<TEntity>>(json);
                resultList.AddRange(results.Entities);
                requestUrl = results.NextPageUrl?.Uri ?? null; 
            }

            return resultList;
        }

    }
}

