using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.Asanate.Domain {
    public class Initiative : NamedEntity {

        public bool IsCompleted { get; set; }

        public int PercentCompleted { get; set; }

        public Project Project { get; set; }

        public virtual List<Tsk> Tsks { get; set; } = new List<Tsk>();
    }
}
