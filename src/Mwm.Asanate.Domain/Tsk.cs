using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Mwm.Asanate.Domain {
    public class Tsk : NamedEntity {

        public Tsk() { }

          public Status Status { get; set; }

        public bool IsArchived { get; set; }

        public bool IsCompleted { get; set; }

        public float? DurationEstimate { get; set; }

        public float? DurationCompleted { get; set; }


        public string Notes { get; set; }

        

        public DateTime? DueDate { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? StartedDate { get; set; }

        public DateTime? CompletedDate { get; set; }

        [JsonIgnore]
        public User? AssignedTo { get; set; }

        public int? AssignedToId { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [JsonIgnore]
        public User? CreatedBy { get; set; }

        public int? CreatedById { get; set; } = User.MeId;


        public DateTime ModifiedDate { get; set; } = DateTime.Now;

        [JsonIgnore]
        public User? ModifiedBy { get; set; }

        public int? ModifiedById { get; set; } = User.MeId;

        [JsonIgnore]
        public Initiative Initiative { get; set; }

        public int InitiativeId { get; set; }

        [JsonIgnore]
        public int PercentageCompleted {
            get {
                if (IsCompleted) return 100;
                if (DurationEstimate.HasValue && DurationCompleted.HasValue) {
                    return (int)(DurationEstimate.Value / DurationCompleted.Value) * 100;
                }
                return 0;
            }
        }

        [JsonIgnore]
        public string ProjectName => Initiative?.Project?.Name ?? string.Empty;

        [JsonIgnore]
        public string CompanyName => Initiative?.Project?.Company?.Name ?? string.Empty;

    }
}
