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

namespace Mwm.MyQ.Application.Projects.Commands {
    public partial class ProjectBase {

        public class Command : IAddEntityCommand<Project> {

            public string Name { get; set; }

            public string Color { get; set; }

            public int CompanyId { get; set; }

        }
    }
}




