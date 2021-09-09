using Mwm.Asana.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.Asana.Service {
    public class AsanaResult<TEntity> where TEntity : IAsanaEntity {

        [JsonProperty("data")]
        public TEntity[] Entities{ get; set; }

        [JsonProperty("next_page")]
        public AsanaUrl NextPageUrl { get; set; }
    }
}
