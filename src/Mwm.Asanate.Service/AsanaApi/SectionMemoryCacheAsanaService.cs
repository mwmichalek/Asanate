using FluentResults;
using Mwm.Asanate.Model;
using Mwm.Asanate.Model.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.Asanate.Service.AsanaApi {

	public interface ISectionAsanaService : IAsanaService<Section> {

	}
    public class SectionMemoryCacheAsanaService : MemoryCacheAsanaService<Section>, ISectionAsanaService {

        private IAsanaService<Project> projectService;

        public SectionMemoryCacheAsanaService(IAsanaHttpClientFactory httpClientFactory, IAsanaService<Project> projectService) :
            base(httpClientFactory) {
            this.projectService = projectService;
        }

        public override async Task<Result> Initialize() {
            try {
                var resultList = new List<Section>();
                var projectsResult = await projectService.RetrieveAll();
                if (projectsResult.IsSuccess) {
                    foreach (var project in projectsResult.Value) {
                        var requestUrl = $"projects/{project.Gid}/sections".ToRetrieveAllUrl();
                        while (requestUrl != null) {
                            var json = await httpClient.GetStringAsync(requestUrl);
                            var results = JsonConvert.DeserializeObject<AsanaResult<Section>>(json);
                            resultList.AddRange(results.Entities);
                            requestUrl = results.NextPageUrl?.Uri ?? null;
                        }
                    }

                    entitiesLookup = resultList.ToDictionary(e => e.Gid);
                    return Result.Ok();
                } else
                    throw new ConfigurationErrorsException($"Unable to retrieve {typeof(Project)}");

            } catch (Exception) {
                throw new ConfigurationErrorsException($"Unable to retrieve {typeof(Project)}");
            }
        }

    }
}

