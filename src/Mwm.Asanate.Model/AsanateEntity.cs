using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.Asanate.Model {
    public abstract class AsanateEntity {

        //public abstract string[] Attributes { get; }

        [JsonProperty("gid")]
        public string Gid { get; set; }

    }
}
