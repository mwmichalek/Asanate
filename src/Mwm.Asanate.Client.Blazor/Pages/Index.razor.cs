using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Mwm.Asanate.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mwm.Asanate.Client.Blazor.Pages {
    public partial class Index {
        [Inject]
        protected HttpClient HttpClient { get; set; }


        public List<Company> Companies = new List<Company>();

        public List<Tsk> Tsks = new List<Tsk>();

        [Inject]
        public IConfiguration Configuration { get; set; }


        protected override async Task OnInitializedAsync() {

            Console.WriteLine($"Hi [{HttpClient.BaseAddress}]");


            try {
                var tskJson = await HttpClient.GetStringAsync("Tsk");
                Console.WriteLine($"TskJson [{tskJson}]");
                Tsks.AddRange(JsonConvert.DeserializeObject<List<Tsk>>(tskJson));
            } catch (Exception ex) {
                Console.WriteLine("Why did you fail, you suck! " + ex);
                Tsks.Add(new Tsk { Name = $"You suck: {ex}" });
            }


            //StateHasChanged();

            //var taskResult = await TskService.RetrieveAll();
            //if (taskResult.IsSuccess) {
            //    Tasks = taskResult.Value.Where(tsk => tsk.Projects.Length > 0 && !tsk.IsCompleted)
            //                            .Select(tsk => new TasksModel {
            //                                Id = tsk.Gid,
            //                                Title = tsk.Name,
            //                                Status = tsk.Status,
            //                                Summary = tsk.Notes,
            //                                Project = tsk.ProjectName,
            //                                Company = tsk.ProjectCompany
            //                            }).ToList();

            //    Companies = Tasks.Select(tsk => tsk.Company).Distinct().ToList();

        }

        public string SwimLaneName(string keyField) {
            var keyAndStatus = keyField.Split("] ");
            if (keyAndStatus.Length > 0) return keyAndStatus[1];
            return keyField;
        }

    }

    public class TasksModel {

        private static Dictionary<string, string> _statusRanked = new Dictionary<string, string> {
            {"Open", "1"},
            {"Planned", "2"},
            {"Queued", "3"},
            {"Ready To Start", "4"},
            {"In Progress", "5"},
            {"Pending", "6"},
            {"Done", "7" }
        };


        public string Id { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }

        public string StatusRank {
            get {
                if (_statusRanked.TryGetValue(Status, out string statusRank))
                    return $"[{statusRank}] {Status}";
                return "";
            }
        }

        public string Summary { get; set; }
        public string Assignee { get; set; }

        public string Project { get; set; }

        public string Company { get; set; }
    }
}
