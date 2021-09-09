using Microsoft.AspNetCore.Components;
using Mwm.Asana.Model;
using Mwm.Asana.Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Mwm.Asanate.Clients.Blazor.Pages {
    public partial class Index {
        [Inject]
        protected IAsanaService<AsanaTsk> TskService { get; set; }

        public List<TasksModel> Tasks = new List<TasksModel>();

        public List<string> Companies = new List<string>();

        protected override async Task OnInitializedAsync() {
            var taskResult = await TskService.RetrieveAll();
            if (taskResult.IsSuccess) {
                Tasks = taskResult.Value.Where(tsk => tsk.Projects.Length > 0 && !tsk.IsCompleted)
                                        .Select(tsk => new TasksModel {
                                            Id = tsk.Gid,
                                            Title = tsk.Name,
                                            Status = tsk.Status,
                                            Summary = tsk.Notes,
                                            Project = tsk.ProjectName,
                                            Company = tsk.ProjectCompany
                                        }).ToList();

                Companies = Tasks.Select(tsk => tsk.Company).Distinct().ToList();
            }
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
