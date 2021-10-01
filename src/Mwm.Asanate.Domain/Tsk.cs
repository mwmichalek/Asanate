using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Mwm.Asanate.Domain {
    public class Tsk : NamedEntity {

        public Tsk() { }

        //Jira
        public string ExternalId { get; set; }

        public Status Status { get; set; }

        public bool IsArchived { get; set; }

        public string Notes { get; set; }

        public DateTime? CompletedDate { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? DueDate { get; set; }

        public DateTime? StartedDate { get; set; }

        [JsonIgnore]
        public Initiative Initiative { get; set; }

        public int InitiativeId { get; set; }

        [JsonIgnore]
        public User? AssignedTo { get; set; }

        public int AssignedToId { get; set; }

        [JsonIgnore]
        public string ProjectName => Initiative?.Project?.Name ?? string.Empty;

        [JsonIgnore]
        public string CompanyName => Initiative?.Project?.Company?.Name ?? string.Empty;

    }
}
