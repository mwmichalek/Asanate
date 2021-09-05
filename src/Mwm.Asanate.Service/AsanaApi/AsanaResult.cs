using Mwm.Asanate.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.Asanate.Service.AsanaApi {
    public class AsanaResult<TEntity> where TEntity : IAsanaEntity {

        [JsonProperty("data")]
        public TEntity[] Entities{ get; set; }

        [JsonProperty("next_page")]
        public AsanaUrl NextPageUrl { get; set; }
    }
}
