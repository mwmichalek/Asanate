using Mwm.Asana.Model.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.Asana.Model {

    public interface IAsanaEntity {

        public string Gid { get; }
    }

    public abstract class NamedAsanaEntity : AsanaEntity {

        [JsonProperty("name")]
        [AsanaProperty("name")]
        public string Name { get; set; }
    }

    public abstract class AsanaEntity : IAsanaEntity {

        [JsonProperty("gid")]
        [AsanaProperty("gid")]
        public string Gid { get; set; }

    }
}
