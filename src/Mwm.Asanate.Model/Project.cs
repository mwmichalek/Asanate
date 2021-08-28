using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace Mwm.Asanate.Model {

    [DataContract(Name = "projects")]
    public class Project : AsanateEntity {

        [JsonProperty("color")]
        public string Color { get; set; }

        private string title;
        [JsonProperty("name")]
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


