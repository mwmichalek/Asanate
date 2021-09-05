using Mwm.Asanate.Model.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.Asanate.Model {

    public class Membership : AsanaEntity {

        [JsonProperty("section")]
        [JsonConverter(typeof(EntityConverter<Section>))]
        public Section Section { get; set; }

        [JsonProperty("project")]
        [JsonConverter(typeof(EntityConverter<Project>))]
        public Project Project { get; set; }

    }
}
