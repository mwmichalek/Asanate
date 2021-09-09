using Mwm.Asana.Model.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.Asana.Model {

    [AsanaEntity(PluralEntityName = "workspaces")]
    public class AsanaWorkspace : NamedAsanaEntity {

        private const string workspaceId = "1153313287544364";

        public static AsanaWorkspace Default => new AsanaWorkspace { Gid = workspaceId };
    }
}
