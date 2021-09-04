using Mwm.Asanate.Model.Attributes;
using Mwm.Asanate.Model.Converters;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Runtime.Serialization;

namespace Mwm.Asanate.Model {

    //Project Gid: 1199690891861721, Company: MWM, Name: Home, Workspace.Gid: 1153313287544364
    //Project Gid: 1199690891861732, Company: MWM, Name: BFV Hardware, Workspace.Gid: 1153313287544364
    //Project Gid: 1199693027751925, Company: MWM, Name: BFV Software, Workspace.Gid: 1153313287544364
    //Project Gid: 1199693007159902, Company: SGN, Name: Command Launcher, Workspace.Gid: 1153313287544364
    //Project Gid: 1153313118644116, Company: KMV, Name: CDPHP, Workspace.Gid: 1153313287544364
    //Project Gid: 1199693027751923, Company: KMV, Name: BOT, Workspace.Gid: 1153313287544364
    //Project Gid: 1200598172934807, Company: LAB49, Name: General, Workspace.Gid: 1153313287544364
    //Project Gid: 1200874933882307, Company: Blackstone, Name: HedgeHog, Workspace.Gid: 1153313287544364

    [AsanaEntity(PluralEntityName = "projects")]
    public class Project : AsanaEntity {

        [JsonProperty("color")]
        [AsanaProperty("color")]
        public string Color { get; set; }

        private string title;

        [JsonProperty("name")]
        [AsanaProperty("name")]
        public string Title {
            get => title;
            set {
                title = value;
                var companyAndName = title.Split(" - ");
                if (companyAndName.Length == 2) {
                    Company = companyAndName[0];
                    Name = companyAndName[1];
                }
            }
        }

        [JsonIgnore]
        public string Name { get; private set; }

        [JsonIgnore]
        public string Company { get; private set; }

        [JsonProperty("workspace")]
        [AsanaProperty("workspace")]
        [JsonConverter(typeof(WorkspaceConverter))]
        public Workspace Workspace => Workspace.Default;

        public static Project[] ToProjectArray(params string[] projectGids) {
            return projectGids.Select(pg => new Project { Gid = pg }).ToArray();
        }

    }
}


