using Newtonsoft.Json;
using Mwm.Asanate.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.Asanate.Model {

    [AsanaEntity(
        PluralEntityName = "tasks",
        AdditionalParameters = "assignee=1153313240116893")]
    public class ATask : AsanaEntity {

        [JsonProperty("completed")]
        [AsanaProperty("completed")]
        public bool IsCompleted { get; set; }

        [JsonProperty("completed_at")]
        [AsanaProperty("completed_at")]
        public DateTime? CompletedAt { get; set; }

        [JsonProperty("created_at")]
        [AsanaProperty("created_at")]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty("due_at")]
        [AsanaProperty("due_at")]
        public DateTime? DueAt { get; set; }

        [JsonProperty("due_on")]
        [AsanaProperty("due_on")]
        public DateTime? DueOn { get; set; }

        [JsonProperty("memberships")]
        [AsanaProperty("memberships.section.name")]
        public Membership[] Memberships { get; set; }

        public string Status => Memberships.FirstOrDefault()?.Section?.Name ?? string.Empty;

        [JsonProperty("modified_at")]
        [AsanaProperty("modified_at")]
        public DateTime? ModifiedAt { get; set; }

        [JsonProperty("name")]
        [AsanaProperty("name")]
        public string Name { get; set; }

        [JsonProperty("start_on")]
        [AsanaProperty("start_on")]
        public DateTime? StartedOn { get; set; }

    }
}





