using Mwm.Asanate.Application.Shared.Commands;
using Mwm.Asanate.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.Asanate.Application.Initiatives.Commands {
    public partial class InitiativeUpdate {
        public class Command : InitiativeBase.Command, IUpdateEntityCommand<Initiative> { 
        
            public int Id {  get; set; }        
        
        }

    }
}
