﻿using Mwm.Asanate.Application.Shared.Commands;
using Mwm.Asanate.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.Asanate.Application.Tsks.Commands {
    public partial class TskUpdate {
        public class Command : TskBase.Command, IUpdateEntityCommand<Tsk> { 
        
            public int Id {  get; set; }        
        
        }

    }
}
