using Mwm.Asanate.Model.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.Asanate.Model {

    [AsanaEntity(PluralEntityName = "workspaces")]
    public class Workspace : NamedAsanaEntity {

        private const string workspaceId = "1153313287544364";

        public static Workspace Default => new Workspace { Gid = workspaceId };
    }
}
