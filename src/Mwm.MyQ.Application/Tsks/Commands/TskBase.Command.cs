using FluentResults;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using Mwm.MyQ.Application.Interfaces.Persistance;
using Mwm.MyQ.Application.Shared.Commands;
using Mwm.MyQ.Application.Shared.Workflows;
using Mwm.MyQ.Application.Utils;
using Mwm.MyQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Mwm.MyQ.Application.Tsks.Commands {

    public partial class TskBase {

        public class Command {

            public string Name { get; set; }

            public string ExternalId { get; set; } 

            public bool? IsArchived { get; set; }

            public bool? IsCompleted { get; set; }

            public bool? IsDeleted { get; set; }

            public bool? IsInFocus { get; set; }    

            public float? DurationEstimate { get; set; }

            public float? DurationCompleted { get; set; }

            public string Notes { get; set; }

            public DateTime? DueDate { get; set; }

            public DateTime? StartDate { get; set; }

            public DateTime? StartedDate { get; set; }

            public DateTime? CompletedDate { get; set; }

            public int? AssignedToId { get; set; } = User.MeId;

            public int? CreatedById { get; set; } = User.MeId;

            public int? ModifiedById { get; set; } = User.MeId;

            public int? InitiativeId { get; set; }

            public List<Activity> Activities { get; set; }

        }
    }
}
