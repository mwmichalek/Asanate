using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.Asanate.Domain {
    public class Company : NamedEntity {

        public string Color { get; set; }

        public virtual List<Project> Projects { get; set; } = new List<Project>();

    }
}
