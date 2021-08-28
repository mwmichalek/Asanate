using Mwm.Asanate.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.Asanate.Service.AsanaApi {
    public class AsanaResult<TEntity> where TEntity : AsanateEntity {

        [JsonProperty("data")]
        public TEntity[] Entities{ get; set; }
        public object next_page { get; set; }
    }
}
