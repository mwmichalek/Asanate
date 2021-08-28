using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.Asanate.Service.Helpers {
    public static class AttributeHelpers {

        public static List<string> GetFieldNames(this Type modelType) {
            return modelType.GetProperties()
                .Select(p => p.GetCustomAttribute<JsonPropertyAttribute>())
                .Where(jp => jp != null)
                .Select(jp => jp.PropertyName)
                .ToList();
        }

        public static string GetFieldNameList(this Type modelType) => string.Join(",", modelType.GetFieldNames());


        public static string GetEntitySetName(this Type modelType) {
            var entitySetName = modelType.GetCustomAttribute<DataContractAttribute>()?.Name;
            return entitySetName ?? throw new NotImplementedException($"Class of type {modelType} is missing [DataContract(Name = 'name goes here')]");
        }
    }
}
