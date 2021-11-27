using FluentResults;
using Mwm.Asana.Model;
using Mwm.Asana.Model.Attributes;
using Mwm.MyQ.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.Asana.Service {

	public interface ISectionAsanaService : IAsanaService<AsanaSection> {

	}
	public class SectionMemoryCacheAsanaService : MemoryCacheAsanaService<AsanaSection>, ISectionAsanaService {

		private IAsanaService<AsanaProject> projectService;

		public SectionMemoryCacheAsanaService(IAsanaHttpClientFactory httpClientFactory, IAsanaService<AsanaProject> projectService) :
			base(httpClientFactory) {
			this.projectService = projectService;
		}

		public override async Task<Result> Initialize() {
			try {
				var resultList = new List<AsanaSection>();
				var projectsResult = await projectService.RetrieveAll();
				if (projectsResult.IsSuccess) {
					foreach (var project in projectsResult.Value) {
						var requestUrl = $"projects/{project.Gid}/sections".ToRetrieveAllUrl();
						while (requestUrl != null) {
							var json = await httpClient.GetStringAsync(requestUrl);
							var results = JsonConvert.DeserializeObject<AsanaResult<AsanaSection>>(json);
							resultList.AddRange(results.Entities);
							requestUrl = results.NextPageUrl?.Uri ?? null;
						}
					}

					entitiesLookup = resultList.ToDictionary(e => e.Gid);
					return Result.Ok();
				} else
					throw new ConfigurationErrorsException($"Unable to retrieve {typeof(AsanaProject)}");

			} catch (Exception) {
				throw new ConfigurationErrorsException($"Unable to retrieve {typeof(AsanaProject)}");
			}
		}

        public override async Task<Result<List<AsanaSection>>> RetrieveAll(DateTime? modifiedSince = null) {
			if (entitiesLookup == null)
				await Initialize();
            return await base.RetrieveAll(modifiedSince);
        }	

	}
}

