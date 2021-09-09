using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.Asana.Model.Attributes {
    public class AsanaEntityAttribute : Attribute {

        public AsanaEntityAttribute() {
        }

        public string PluralEntityName { get; set; }

        public string? AdditionalParameters { get; set; }

    }
}
