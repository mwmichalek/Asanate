using Mwm.Asanate.Application.Shared.Commands;
using Mwm.Asanate.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.Asanate.Application.Tsks.Commands {
    public partial class TskUpdate {
        public class Command : IUpdateEntityCommand<Tsk> {

            public string Name { get; set; }

            public string? ExternalId { get; set; }

            public Status Status { get; set; }

            public bool? IsArchived { get; set; }

            public bool? IsCompleted { get; set; }

            public int? DurationEstimate { get; set; }

            public int? DuractionCompleted { get; set; }

            public int? PercentageCompleted { get; set; }

            public string? Notes { get; set; }

            public DateTime? DueDate { get; set; }

            public DateTime? StartDate { get; set; }

            public DateTime? StartedDate { get; set; }

            public DateTime? CompletedDate { get; set; }

            public int? AssignedToId { get; set; }

            public int? CreatedById { get; set; }

            public int? ModifiedById { get; set; }

            public int? InitiativeId { get; set; }

        }
    }
}
