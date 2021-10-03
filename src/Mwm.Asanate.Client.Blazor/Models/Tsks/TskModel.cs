using Mwm.Asanate.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mwm.Asanate.Client.Blazor.Models.Tsks {
    public class TskModel {

        public int Id { get; set; }

        public string Name { get; set; }

        public int EstimatedHours { get; set; }

        public int ActualHours { get; set; }

        public string ExternalId { get; set; }

        public Status Status { get; set; }

        public int StatusId => (int)Status;

        public string StatusName {
            get {
                return Status.ToString();
            }
            set {
                Status = value.ToStatus();
            }
        }

        public bool IsArchived { get; set; }

        public string Notes { get; set; }

        public DateTime? CompletedDate { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? DueDate { get; set; }

        public DateTime? StartedDate { get; set; }

        public string InitiativeName { get; set; }

        public string ProjectName { get; set; }

        public string CompanyName { get; set; }
    }
}
