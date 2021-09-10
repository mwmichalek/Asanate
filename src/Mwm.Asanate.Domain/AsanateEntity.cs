using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.Asanate.Domain {

    public interface IAsanateEntity {

        public uint Id { get; }

        public string Gid { get; }

    }

    public class AsanateEntity : IAsanateEntity {

        public uint Id { get; set; }

        public string Gid { get; set; }

    }


}
