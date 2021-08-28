using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace Mwm.Asanate.Model {

    [DataContract(Name = "projects")]
    public class Project : AsanateEntity {

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        //public override string[] Attributes => new string[] { "gid", "name", "color" };
    }
}


