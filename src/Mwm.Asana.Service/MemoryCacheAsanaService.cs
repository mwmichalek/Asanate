using FluentResults;
using Mwm.Asana.Model;
using Mwm.Asana.Service;
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
using Mwm.MyQ.Common.Utils;

namespace Mwm.MyQ.Service {

    public class MemoryCacheAsanaService<TEntity> : AsanaService<TEntity> where TEntity : IAsanaEntity {

        protected DateTime previousFetchTime;

        protected Dictionary<string, TEntity> entitiesLookup;

        public MemoryCacheAsanaService(IAsanaHttpClientFactory httpClientFactory) : base(httpClientFactory) {
        }

        public override async Task<Result> Initialize() {
            var allEntitiesResult = await base.RetrieveAll();
            if (allEntitiesResult.IsSuccess) {

                entitiesLookup = allEntitiesResult.Value.ToDictionary(e => e.Gid);
                previousFetchTime = DateTime.Now;
                return Result.Ok();
            }
            throw new ConfigurationErrorsException($"Unable to retrieve {typeof(TEntity)}");
        }

        public override async Task<Result<List<TEntity>>> RetrieveAll(DateTime? modifiedSince = null) {
            if (entitiesLookup == null)
                await Initialize();

            // Retreive mods since previous fetch.
            var updatedEntitiesResult = await base.RetrieveAll(previousFetchTime);
            previousFetchTime = DateTime.Now;

            // Update cache with new values
            if (updatedEntitiesResult.TryUsing(out List<TEntity> entities)) {
                foreach (var entity in entities)
                    entitiesLookup[entity.Gid] = entity;
            }

            // Only return entities after modifiedSince
            if (modifiedSince != null)
                return Result.Ok(entitiesLookup.Values.Where(e => e.ModifiedAt >= modifiedSince).ToList());
            return Result.Ok(entitiesLookup.Values.ToList());
        }

    }
}
