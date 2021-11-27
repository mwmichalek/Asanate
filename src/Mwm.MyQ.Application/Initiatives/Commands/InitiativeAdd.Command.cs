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

    public partial class InitiativeAdd {

        public class Command : InitiativeBase.Command, IAddEntityCommand<Initiative> { }

    }
}




