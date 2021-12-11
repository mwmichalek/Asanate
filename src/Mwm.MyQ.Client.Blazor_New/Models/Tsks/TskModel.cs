using Mwm.MyQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mwm.MyQ.Client.Blazor.Models.Tsks {
    public class TskModel {

        public int Id { get; set; }

        public string Name { get; set; }

        public Status Status { get; set; }

        public int StatusId => (int)Status;

        public string StatusName {
            get {
                return Status.ToStr();
            }
            set {
                Status = value.ToStatus();
            }
        }

        public bool IsArchived { get; set; }

        // IsCompleted

        //public int? PercentCompleted { get; set; }

        public float? DurationEstimate { get; set; }

        public float? DurationCompleted { get; set; }

        public string Notes { get; set; }

        public DateTime? DueDate { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? StartedDate { get; set; }

        public DateTime? CompletedDate { get; set; }

        public int? InitiativeId { get; set; }

        public string InitiativeName { get; set; }

        public string InitiativeExternalId { get; set; }


        public int? ProjectId { get; set; }

        public string ProjectName { get; set; }

        public string CompanyName { get; set; }
    }
}
