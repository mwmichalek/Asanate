using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.Asanate.Model {

    public interface INamedEntity {
        public string Name { get; }

    }
    public class NamedEntity : AsanateEntity {

        public string Name { get; set; }

    }
}
