using Mwm.Asanate.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;

namespace Mwm.Asanate.Service.AsanaApi {
    public class AsanaService : IAsanaService {

        private const string token = "1/1153313240116893:e88b2654eff760fb62c702fd4f5502e4";
        private const string base_url = "https://app.asana.com/api/1.0/";

        private HttpClient client;

        private HttpClient Client {
            get {
                if (client == null) {
                    client = new HttpClient();
                    client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", token);
                    client.BaseAddress = new Uri(base_url);
                }
                return client;
            }
        }

        public async Task<List<TEntity>> GetAll<TEntity>() where TEntity : AsanateEntity {
            var fields = GetFields(typeof(TEntity));
            var json = await Client.GetStringAsync($"projects?opt_fields={fields}&limit=10&workspace=1153313287544364");
            var results = JsonConvert.DeserializeObject<AsanaResult<TEntity>>(json);
            return results.Entities.ToList();
        }

        private string GetFields(Type modelType) {
            var propertyNames = modelType.GetProperties()
                .Select(p => p.GetCustomAttribute<JsonPropertyAttribute>())
                .Select(jp => jp.PropertyName)
                .ToList();
            return string.Join(",", propertyNames);
        }
    }
}
