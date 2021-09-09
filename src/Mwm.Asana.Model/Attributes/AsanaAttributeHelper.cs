using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.Asana.Model.Attributes {
    public static class AsanaAttributeHelper {

        // modified_since=2021-09-04T12:00:00

        public static string ToRetrieveAllUrl(this Type modelType,
                                              DateTime? modifiedSince = null,
                                              string suppliedBaseUrl = null) {
            var pluralEntityName = modelType.GetPluralEntityName();
            var properties = modelType.GetPropertyNameList();
            var additionalParameters = modelType.GetAdditionalParameters();
            return ToRetrieveAllUrl(pluralEntityName, properties, additionalParameters);
        }

        public static string ToRetrieveAllUrl(this string baseUrl,
                                              string properties = null,
                                              string additionalParameters = null,
                                              DateTime? modifiedSince = null) {
            var requestBuilder = new StringBuilder();
            requestBuilder.Append($"{baseUrl}?workspace={AsanaWorkspace.Default.Gid}"); //limit=99&

            if (properties != null)
                requestBuilder.Append($"&opt_fields={properties}");
            if (!string.IsNullOrEmpty(additionalParameters))
                requestBuilder.Append($"&{additionalParameters}");
            if (modifiedSince.HasValue) {
                var dt = modifiedSince.Value;
                requestBuilder.Append($"&modified_since={dt.Year}-{dt.Month}-{dt.Day}T{dt.Hour}:{dt.Minute}:{dt.Second}");
            }
            return requestBuilder.ToString();
        }

        public static string ToPersistUrl(this Type modelType) {
            var pluralEntityName = modelType.GetPluralEntityName();
            return $"{pluralEntityName}";
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
