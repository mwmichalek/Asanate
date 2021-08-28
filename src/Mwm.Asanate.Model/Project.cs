using Mwm.Asanate.Model.Attributes;
using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace Mwm.Asanate.Model {

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

        public string Name { get; private set; }
        public string Company { get; private set; }
    }
}


