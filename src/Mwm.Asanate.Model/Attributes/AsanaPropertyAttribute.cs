using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.Asana.Model.Attributes {
    public class AsanaPropertyAttribute : Attribute {

        public AsanaPropertyAttribute(string propertyName) {
            PropertyName = propertyName;
        }

        public string PropertyName { get; set; }
    }
}
