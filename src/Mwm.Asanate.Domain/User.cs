using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.Asanate.Domain {
    public class User : NamedEntity {

        public static readonly int MeId = 1;

        public string FirstName { get; set; }

        public string LastName { get; set; }

    }
}
