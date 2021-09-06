using FluentResults;
using Mwm.Asanate.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.Asanate.Service .AsanaApi {

    public class MemoryCacheAsanaService<TEntity> : AsanaService<TEntity> where TEntity : IAsanaEntity {

        protected DateTime previousFetchTime;

        protected Dictionary<string, TEntity> entitiesLookup;

        public MemoryCacheAsanaService(IAsanaHttpClientFactory httpClientFactory) : base(httpClientFactory) {
        }

        public override async Task<Result> Initialize() {
            var allEntitiesResult = await base.RetrieveAll();
            if (allEntitiesResult.IsSuccess) {
                entitiesLookup = allEntitiesResult.Value.ToDictionary(e => e.Gid);
                return Result.Ok();
            }
            throw new ConfigurationErrorsException($"Unable to retrieve {typeof(TEntity)}");
        }

        public override Task<Result<List<TEntity>>> RetrieveAll(DateTime? modifiedSince = null) {
            return Task.FromResult(Result.Ok(entitiesLookup.Values.ToList()));
        }

    }
}
