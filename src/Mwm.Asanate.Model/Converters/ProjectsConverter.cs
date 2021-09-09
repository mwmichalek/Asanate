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
    public class ProjectsConverter : Newtonsoft.Json.JsonConverter {

        public override bool CanConvert(Type objectType) {
            return (objectType == typeof(AsanaProject[]));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            var token = JToken.Load(reader);
            if (token.Type == JTokenType.Array) 
                return token.ToObject<AsanaProject[]>();
            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            if (value is AsanaProject[] projects) {
                var idArray = projects.Select(p => p.Gid).ToArray();
                serializer.Serialize(writer, idArray);
            }
        }
    }
}
