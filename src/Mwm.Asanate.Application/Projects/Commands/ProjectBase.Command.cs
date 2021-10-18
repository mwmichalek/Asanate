using MediatR;
using System;
using FluentResults;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mwm.Asanate.Application.Shared.Commands;
using Mwm.Asanate.Domain;
using Microsoft.Extensions.Logging;
using Mwm.Asanate.Application.Interfaces.Persistance;
using Mwm.Asanate.Application.Utils;
using System.Threading;

namespace Mwm.Asanate.Application.Projects.Commands {
    public partial class ProjectBase {

        public class Command : IAddEntityCommand<Project> {

            public string Name { get; set; }

            public string Color { get; set; }

            public int? CompanyId { get; set; }

        }
    }
}




