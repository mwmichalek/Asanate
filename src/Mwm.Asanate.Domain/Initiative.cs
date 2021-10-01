using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Mwm.Asanate.Domain {
    public class Initiative : NamedEntity {

        public readonly static string DefaultInitiativeName = "Triage";

        public string ExternalId { get; set; }

        public bool IsCompleted { get; set; }

        public int PercentCompleted { get; set; }

        [JsonIgnore]
        public Project Project { get; set; }

        public int ProjectId { get; set; }

        [JsonIgnore]
        public virtual List<Tsk> Tsks { get; set; } = new List<Tsk>();
    }
}
