using Mwm.Asanate.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Mwm.Asanate.Model.Converters {
    public class WorkspaceConverter : Newtonsoft.Json.JsonConverter {

        public override bool CanConvert(Type objectType) {
            return (objectType == typeof(AsanaWorkspace));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            var token = JToken.Load(reader);
            if (token.Type == JTokenType.Object) 
                return token.ToObject<AsanaWorkspace>();
            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            if (value is AsanaWorkspace workspace) 
                serializer.Serialize(writer, workspace.Gid);
        }
    }
}
