using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.MyQ.Domain {

    public interface INamedEntity : IEntity {
        public string Name { get; set; }

    }
    public class NamedEntity : Entity, INamedEntity {

        [Column(Order = 1)]
        public string Name { get; set; }

    }
}
