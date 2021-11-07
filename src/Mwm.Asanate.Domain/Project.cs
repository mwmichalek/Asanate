using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Mwm.Asanate.Domain {
    public class Project : NamedEntity {

        public readonly static string DefaultProjectName = "Generic";

        public string ExternalIdPrexfix { get; set; }

        public string Color { get; set; }

        public int CompanyId { get; set; }

        [JsonIgnore]
        public Company Company { get; set; }

        public virtual List<Initiative> Initiatives { get; set; } = new List<Initiative>();

    }
}
