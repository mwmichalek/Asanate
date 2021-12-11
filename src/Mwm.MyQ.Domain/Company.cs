using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Mwm.MyQ.Domain {
    public class Company : NamedEntity {

        

        public static readonly string PersonalCompanyName = "MWM";

        public static readonly string DefaultCompanyName = PersonalCompanyName;

        public bool IsPersonal { get; set; }

        public string Color { get; set; }

        [JsonIgnore]
        public virtual List<Project> Projects { get; set; } = new List<Project>();

    }
}
