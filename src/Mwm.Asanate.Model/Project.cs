using Newtonsoft.Json;
using System;

namespace Mwm.Asanate.Model {
    public class Project : AsanateEntity {

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        //public override string[] Attributes => new string[] { "gid", "name", "color" };
    }
}


