using MediatR;
using System;
using FluentResults;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mwm.MyQ.Application.Shared.Commands;
using Mwm.MyQ.Domain;
using Mwm.MyQ.Application.Interfaces.Persistance;
using Mwm.MyQ.Application.Utils;
using System.Threading;

namespace Mwm.MyQ.Application.Companies.Commands {
    public partial class CompanyBase {

        public class Command : IAddEntityCommand<Company> {

            public string Name { get; set; }

            public bool? IsPersonal { get; set; }

            public string Color { get; set; }

        }
    }
}




