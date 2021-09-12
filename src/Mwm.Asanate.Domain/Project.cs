using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.Asanate.Domain {
    public class Project : NamedEntity {

        public string Color { get; set; }

        public Company Company { get; set; }

        public virtual List<Initiative> Initiatives { get; set; } = new List<Initiative>();

        // public Workspace Workspace { get; set; }

    }
}
