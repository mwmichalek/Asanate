using Mwm.Asanate.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.Asanate.Service.AsanaApi {
    public class AsanaRequest<TEntity> where TEntity : AsanaEntity {

        public AsanaRequest(TEntity entity) {
            Entity = entity;
        }


        [JsonProperty("data")]
        public TEntity Entity { get; set; }
    }
}
