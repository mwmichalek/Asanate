using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.Asanate.Domain {

    public interface INamedEntity : IEntity {
        public string Name { get; }

    }
    public class NamedEntity : Entity, INamedEntity {

        public string Name { get; set; }

    }
}
