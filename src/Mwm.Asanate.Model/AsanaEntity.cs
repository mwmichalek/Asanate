using Mwm.Asanate.Model.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.Asanate.Model {
    public abstract class AsanaEntity {

        [JsonProperty("gid")]
        [AsanaProperty("gid")]
        public string Gid { get; set; }

    }
}
