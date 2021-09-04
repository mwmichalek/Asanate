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

    public interface IAsanaService {

        Task<Result<List<TEntity>>> RetrieveAll<TEntity>(DateTime? modifiedSince = null) where TEntity : AsanaEntity;

    }

    public class AsanaService : IAsanaService {

        private HttpClient httpClient;

        public AsanaService(HttpClient httpClient) {
            this.httpClient = httpClient;
        }

        public async Task<Result<List<TEntity>>> RetrieveAll<TEntity>(DateTime? modifiedSince = null) where TEntity : AsanaEntity {
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

        public async Task<Result<TEntity>> Persist<TEntity>(TEntity entity) where TEntity : AsanaEntity {
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

    }
}

