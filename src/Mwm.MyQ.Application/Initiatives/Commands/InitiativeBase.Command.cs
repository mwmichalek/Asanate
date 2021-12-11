using MediatR;
using System;
using FluentResults;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mwm.MyQ.Application.Shared.Commands;
using Mwm.MyQ.Domain;
using Microsoft.Extensions.Logging;
using Mwm.MyQ.Application.Interfaces.Persistance;
using Mwm.MyQ.Application.Utils;
using System.Threading;

namespace Mwm.MyQ.Application.Initiatives.Commands {
    public partial class InitiativeBase {

        public class Command : IAddEntityCommand<Initiative> {

            public string Name { get; set; }

            public string? ExternalId { get; set; }

            public bool? IsArchived { get; set; }

            public int? PercentCompleted { get; set; }

            public string? Notes { get; set; }

            public DateTime? CompletedDate { get; set; }

            public DateTime? DueDate { get; set; }

            public DateTime? StartedDate { get; set; }

            public bool? IsComplete { get; set; }


            public int? ProjectId { get; set; }

            public int? AssignedToId { get; set; }

        }
    }
}




