using FluentResults;
using Mwm.Asanate.Model;
using Mwm.Asanate.Model.Attributes;
using Mwm.Asanate.Model.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.Asanate.Service.AsanaApi {

    public interface IAsanaService<TEntity> where TEntity : IAsanaEntity {

        Task<Result<List<TEntity>>> RetrieveAll(DateTime? modifiedSince = null);

        Task<Result<TEntity>> Persist(TEntity entity);

        Task<Result> Initialize();

    }

    public class AsanaService<TEntity> : IAsanaService<TEntity> where TEntity : IAsanaEntity {

        protected HttpClient httpClient;

        public AsanaService(IAsanaHttpClientFactory httpClientFactory) {
            this.httpClient = httpClientFactory.CreateClient();
        }

        public virtual async Task<Result<List<TEntity>>> RetrieveAll(DateTime? modifiedSince = null) {
            try {
                var requestUrl = typeof(TEntity).ToRetrieveAllUrl(modifiedSince);
                var resultList = new List<TEntity>();
                while (requestUrl != null) {
                    var json = await httpClient.GetStringAsync(requestUrl);
                    var results = JsonConvert.DeserializeObject<AsanaResult<TEntity>>(json);
                    resultList.AddRange(results.Entities);
                    requestUrl = results.NextPageUrl?.Uri ?? null;
                }
                return Result.Ok(resultList);
            } catch (Exception) {
                return Result.Fail($"Unable to retrieve {typeof(TEntity)}.");
            }
        }

        public virtual async Task<Result<TEntity>> Persist(TEntity entity) {
            try {
                var requestUrl = typeof(TEntity).ToPersistUrl();
                var persistRequest = new AsanaRequest<TEntity>(entity);
                var entityJson = persistRequest.ToJson();
                var entityJsonContent = new StringContent(entityJson, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(requestUrl, entityJsonContent);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var asanaResponse = JsonConvert.DeserializeObject<AsanaRequest<TEntity>>(responseBody);
                return Result.Ok(asanaResponse.Entity);
            } catch (Exception ex) {
                return Result.Fail($"Unable to persist {typeof(TEntity)}.")
                    .WithError(ex.ToString());
            }
        }

        public virtual Task<Result> SubscribeToUpdates() {
            return Task.FromResult(Result.Ok());
        }

        public virtual Task<Result> Initialize() => Task.FromResult(Result.Ok());

    }
}


//public class Rootobject {
//    public Data data { get; set; }
//}

//public class Data {
//    public Filter[] filters { get; set; }
//    public string resource { get; set; }
//    public string target { get; set; }
//}

//public class Filter {
//    public string action { get; set; }
//    public string[] fields { get; set; }
//    public string resource_subtype { get; set; }
//    public string resource_type { get; set; }
//}

