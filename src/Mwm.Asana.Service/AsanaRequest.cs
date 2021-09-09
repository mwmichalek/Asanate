using Mwm.Asana.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.Asana.Service {
    public class AsanaRequest<TEntity> where TEntity : IAsanaEntity {

        public AsanaRequest(TEntity entity) {
            Entity = entity;
        }


        [JsonProperty("data")]
        public TEntity Entity { get; set; }
    }
}
