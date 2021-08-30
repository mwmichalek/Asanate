using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.Asanate.Model.Attributes {
    public static class AsanaAttributeHelper {

  
        private const string workspaceId = "1153313287544364";

        public static string GetUrl(this Type modelType) {
            var pluralEntityName = modelType.GetPluralEntityName();
            var properties = modelType.GetPropertyNameList();
            var additionalParameters = modelType.GetAdditionalParameters();
            var request = $"{pluralEntityName}?opt_fields={properties}&limit=50&workspace={workspaceId}";
            if (!string.IsNullOrEmpty(additionalParameters))
                request += $"&{additionalParameters}";
            return request;
        }

        public static List<string> GetPropertyNames(this Type modelType) {
            return modelType.GetProperties()
                .Select(p => p.GetCustomAttribute<AsanaPropertyAttribute>())
                .Where(jp => jp != null)
                .Select(jp => jp.PropertyName)
                .ToList();
        }

        public static string GetPropertyNameList(this Type modelType) => string.Join(",", modelType.GetPropertyNames());


        public static string GetPluralEntityName(this Type modelType) {
            var pluralEntityName = modelType.GetCustomAttribute<AsanaEntityAttribute>()?.PluralEntityName;
            return pluralEntityName ?? throw new NotImplementedException($"Class of type {modelType} is missing [AsanaEntity(PluralEntityName = 'name goes here')]");
        }

        public static string? GetAdditionalParameters(this Type modelType) {
            return modelType.GetCustomAttribute<AsanaEntityAttribute>()?.AdditionalParameters;
        }
    }
}
