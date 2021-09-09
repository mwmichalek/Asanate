using Mwm.Asana.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.Asana.Model {
    // User Gid: 1153313240116893, Name: Mark Michalek

    [AsanaEntity(PluralEntityName = "users")]
    public class AsanaUser : NamedAsanaEntity {

        public static AsanaUser Me => new AsanaUser { Gid = "1153313240116893" };

    }
}
