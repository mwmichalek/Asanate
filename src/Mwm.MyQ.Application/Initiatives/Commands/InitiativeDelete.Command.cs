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


namespace Mwm.MyQ.Application.Initiatives.Commands {

    public partial class InitiativeDelete {

        public class Command : IDeleteEntityCommand<Initiative> {  

            public int Id {  get; set; }    
        
        }

    }
}
