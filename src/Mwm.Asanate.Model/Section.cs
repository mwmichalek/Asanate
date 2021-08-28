using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.Asanate.Model {
    public class Section {

        [JsonProperty("name")]
        public string Name { get; set; }

    }
}
