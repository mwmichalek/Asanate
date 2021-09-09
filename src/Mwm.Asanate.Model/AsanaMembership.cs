using Mwm.Asana.Model.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.Asana.Model {

    public class AsanaMembership : AsanaEntity {

        [JsonProperty("section")]
        [JsonConverter(typeof(EntityConverter<AsanaSection>))]
        public AsanaSection Section { get; set; }

        [JsonProperty("project")]
        [JsonConverter(typeof(EntityConverter<AsanaProject>))]
        public AsanaProject Project { get; set; }

    }
}
