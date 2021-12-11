using Mwm.MyQ.Application.Shared.Commands;
using Mwm.MyQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.MyQ.Application.Initiatives.Commands {
    public partial class InitiativeUpdate {
        public class Command : InitiativeBase.Command, IUpdateEntityCommand<Initiative> { 
        
            public int Id {  get; set; }        
        
        }

    }
}
