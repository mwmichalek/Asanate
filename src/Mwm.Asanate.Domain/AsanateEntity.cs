using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.Asanate.Domain {

    public interface IAsanateEntity {

        public string Id { get; }

    }

    public class AsanateEntity : IAsanateEntity {

        public string Id { get; set; }

    }


}
