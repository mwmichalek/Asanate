using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.Asana.Model.Converters {
    public static class JsonConverterExtensions {

        public static T To<T>(this string json) {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static string ToJson(this object obj) {
            return JsonConvert.SerializeObject(obj,
                Newtonsoft.Json.Formatting.None,
                new JsonSerializerSettings {
                    NullValueHandling = NullValueHandling.Ignore
                });
        }
    }
}
