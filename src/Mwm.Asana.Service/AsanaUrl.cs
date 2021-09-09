using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.Asana.Service {
    public class AsanaUrl {

        [JsonProperty("offset")]
        public string OffSet { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }
    }
}
