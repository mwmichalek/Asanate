using FluentResults;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using Mwm.Asanate.Application.Interfaces.Persistance;
using Mwm.Asanate.Application.Shared.Commands;
using Mwm.Asanate.Application.Shared.Workflows;
using Mwm.Asanate.Application.Utils;
using Mwm.Asanate.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Mwm.Asanate.Application.Tsks.Commands {

    public partial class TskBase {

        public class Command {

            public string Name { get; set; }

            public string ExternalId { get; set; } 

            public Status Status { get; set; } 

            public bool? IsArchived { get; set; }

            public bool? IsCompleted { get; set; }

            public int? DurationEstimate { get; set; }

            public int? DurationCompleted { get; set; }

            public string Notes { get; set; }

            public DateTime? DueDate { get; set; }

            public DateTime? StartDate { get; set; }

            public DateTime? StartedDate { get; set; }

            public DateTime? CompletedDate { get; set; }

            public int? AssignedToId { get; set; } = User.MeId;

            public int? CreatedById { get; set; } = User.MeId;

            public int? ModifiedById { get; set; } = User.MeId;

            public int? InitiativeId { get; set; }

        }
    }
}
