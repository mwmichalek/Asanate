using FluentResults;
using Mwm.Asanate.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.Asanate.Service .AsanaApi {
    [DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
    public class FileCacheAsanaService<TEntity> : AsanaService<TEntity> where TEntity : IAsanaEntity {

        private const string CACHE_DIRECTORY = @"C:\Projects\MWM\Asanate\Mwm.Asanate\data";
        private DateTime previousFetchTime;

        private Dictionary<string, TEntity> entitiesLookup;

        public FileCacheAsanaService(IAsanaHttpClientFactory httpClientFactory) : base(httpClientFactory) {
            Task.WaitAll(Initialize());
        }

        public override async Task<Result> Initialize() {
            var jsonPath = Path.Join(CACHE_DIRECTORY, $"{typeof(TEntity)}.json");
            if (File.Exists(jsonPath)) {
                var entitiesLookupJson = await File.ReadAllTextAsync(jsonPath);
                entitiesLookup = JsonConvert.DeserializeObject<Dictionary<string, TEntity>>(entitiesLookupJson);
                var timestamp = File.GetLastWriteTime(jsonPath);
                var updatedEntitiesResult = await RetrieveAll(timestamp);
                if (updatedEntitiesResult.IsSuccess) {
                    foreach (var updatedEntity in updatedEntitiesResult.Value)
                        entitiesLookup[updatedEntity.Gid] = updatedEntity;
                }
            } else {
                var allEntitiesResult = await RetrieveAll();
                if (allEntitiesResult.IsSuccess) {
                    entitiesLookup = allEntitiesResult.Value.ToDictionary(e => e.Gid);
                    var entitiesLookupJson = JsonConvert.SerializeObject(entitiesLookup);
                    await File.WriteAllTextAsync(jsonPath, entitiesLookupJson);
                }
            }
            return Result.Ok();
        }

        public override Task<Result<List<TEntity>>> RetrieveAll(DateTime? modifiedSince = null) {

            //TODO:(MWM) Retrieve updates since last fetch and incorporate with local cache;


            return base.RetrieveAll(modifiedSince);
        }

        private string GetDebuggerDisplay() {
            return ToString();
        }
    }
}
