using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Mwm.MyQ.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mwm.MyQ.Client.Blazor.Pages {
    public partial class Index : ComponentBase {
        [Inject]
        protected HttpClient HttpClient { get; set; }


        public List<string> Companies = new List<string>();

        public List<TasksModel> Tasks = new List<TasksModel>();

        [Inject]
        public IConfiguration Configuration { get; set; }

        protected override async Task OnInitializedAsync() {

            await Task.Run(() => { Console.WriteLine($"Hi [{HttpClient.BaseAddress}]"); });
            //try {
            //    var tskJson = await HttpClient.GetStringAsync("Tsk");
            //    var tsks = JsonConvert.DeserializeObject<List<Tsk>>(tskJson);

            //    Console.WriteLine(tskJson);
            //    Console.WriteLine($"Tsk Count: {tsks?.Count}");

            //    Tasks = tsks.Where(tsk => !tsk.IsArchived)
            //                .Select(tsk => new TasksModel {
            //                    Id = tsk.Gid,
            //                    Title = tsk.Name,
            //                    Status = tsk.Status.ToString(),
            //                    Summary = tsk.Notes,
            //                    Project = tsk.ProjectName,
            //                    Company = tsk.CompanyName
            //                }).ToList();

            //    Console.WriteLine($"Tasks Count: {Tasks.Count}");
            //    Console.WriteLine($"Status: [{string.Join(", ", tsks.Select(t => t.Status).Distinct())}]");

            //    var companiesJson = await HttpClient.GetStringAsync("Company");
            //    var companies = JsonConvert.DeserializeObject<List<Company>>(companiesJson);
            //    Console.WriteLine($"Company Count: {companies?.Count}");

            //    Companies = companies.Select(c => c.Name).ToList();

            //} catch (Exception ex) {
            //    Console.WriteLine("Why did you fail, you suck! " + ex);
            //}

            //StateHasChanged();
        }

        //public string SwimLaneName(string keyField) {
        //    var keyAndStatus = keyField.Split("] ");
        //    if (keyAndStatus.Length > 0) return keyAndStatus[1];
        //    return keyField;
        //}

    }

    public class TasksModel {

  
        public string Id { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }

        public string Summary { get; set; }
        public string Assignee { get; set; }

        public string Project { get; set; }

        public string Company { get; set; }
    }
}
