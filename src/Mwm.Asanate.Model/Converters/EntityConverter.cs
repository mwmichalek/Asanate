using Mwm.Asana.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Mwm.Asana.Model.Converters {
    public class EntityConverter<TEntity> : Newtonsoft.Json.JsonConverter where TEntity : AsanaEntity {

        public override bool CanConvert(Type objectType) {
            return (objectType == typeof(TEntity));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            var token = JToken.Load(reader);
            if (token.Type == JTokenType.Object) 
                return token.ToObject<TEntity>();
            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            if (value is TEntity entity) 
                serializer.Serialize(writer, entity.Gid);
        }
    }
}
