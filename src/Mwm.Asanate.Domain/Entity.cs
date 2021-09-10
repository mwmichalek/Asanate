using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.Asanate.Domain {

    public interface IEntity {

        public uint Id { get; }

        public string? Gid { get; }

    }

    public class Entity : IEntity {

        public uint Id { get; set; }

        public string? Gid { get; set; }
    }




}
