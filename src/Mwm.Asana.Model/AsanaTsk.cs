using Mwm.Asana.Model.Attributes;
using Mwm.Asana.Model.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.Asana.Model {
    //{
    //  "data": {
    //    "name": "Buy catnip 9",
    //    "projects": [
    //      "1200874933882307"
    //    ],
    //    "workspace": "1153313287544364"
    //  }
    //}

    //{
    //  "data": {
    //    "name": "Buy catnip 1000",
    //    "memberships": [{
    //        "project": "1200874933882307",
    //        "section": "1200874933882312"
    //    }],

    //    "workspace": "1153313287544364"
    //  }
    //}

    [AsanaEntity(
        PluralEntityName = "tasks",
        AdditionalParameters = "assignee=1153313240116893")]
    public class AsanaTsk : AsanaEntity {

        [JsonProperty("notes")]
        [AsanaProperty("notes")]
        public string Notes { get; set; }

        [JsonProperty("completed")]
        [AsanaProperty("completed")]
        public bool IsCompleted { get; set; }

        [JsonProperty("completed_at")]
        [AsanaProperty("completed_at")]
        public string CompletedAt { get; set; }

        [JsonProperty("created_at")]
        [AsanaProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("due_at")]
        [AsanaProperty("due_at")]
        public string DueAt { get; set; }

        [JsonProperty("due_on")]
        [AsanaProperty("due_on")]
        public string DueOn { get; set; }

        [JsonProperty("start_on")]
        [AsanaProperty("start_on")]
        public string StartedOn { get; set; }

        [JsonProperty("memberships")]
        [AsanaProperty("memberships.section.name")]
        public AsanaMembership[] Memberships { get; set; }

        [JsonIgnore]
        public string Status => Memberships?.FirstOrDefault()?.Section?.Name ?? string.Empty;

        [JsonProperty("projects")]
        [AsanaProperty("projects.name")]
        [JsonConverter(typeof(EntityArrayConverter<AsanaProject>))]
        public AsanaProject[] Projects { get; set; }

        [JsonIgnore]
        public string ProjectName => Projects?.FirstOrDefault()?.Name ?? string.Empty;

        [JsonIgnore]
        public string ProjectCompany => Projects?.FirstOrDefault()?.Company ?? string.Empty;

        [JsonProperty("modified_at")]
        [AsanaProperty("modified_at")]
        public DateTime? ModifiedAt { get; set; }

        private string name;

        [JsonProperty("name")]
        [AsanaProperty("name")]
        public string Name {
            get => name;
            set {
                name = value;
                var subProjectAndTask = name.Split(" - ");
                if (subProjectAndTask.Length == 2) {
                    SubProjectName = subProjectAndTask[0];
                    name = subProjectAndTask[1];
                }
            }
        }

        [JsonProperty("assignee")]
        [AsanaProperty("assignee.name")]
        [JsonConverter(typeof(EntityConverter<AsanaUser>))]
        public AsanaUser AssignedTo { get; set; }

        [JsonIgnore]
        public string SubProjectName { get; private set; }

        [JsonProperty("workspace")]
        [AsanaProperty("workspace")]
        [JsonConverter(typeof(EntityConverter<AsanaWorkspace>))]
        public AsanaWorkspace Workspace => AsanaWorkspace.Default;

        public override string ToString() {
            return $"Name: {Name:-20}, Status: {Status: -20}, ModifiedAt: {ModifiedAt: -20}, ProjectName: {ProjectName}, SubProjectName: {SubProjectName}, ProjectCompany: {ProjectCompany}";
        }

    }
}





