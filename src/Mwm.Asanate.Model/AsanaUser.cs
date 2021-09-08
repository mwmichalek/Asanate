using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mwm.Asanate.Model.Attributes;

namespace Mwm.Asanate.Model {

    // User Gid: 1153313240116893, Name: Mark Michalek

    [AsanaEntity(PluralEntityName = "users")]
    public class AsanaUser : NamedAsanaEntity {

        public static AsanaUser Me => new AsanaUser { Gid = "1153313240116893" };

    }
}
