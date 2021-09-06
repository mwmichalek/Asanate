using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.Asanate.Service.AsanaApi {

    public interface IAsanaHttpClientFactory {

        HttpClient CreateClient();
    }

    public class AsanaHttpClientFactory : IAsanaHttpClientFactory {

        private const string token = "1/1153313240116893:e88b2654eff760fb62c702fd4f5502e4";
        private const string base_url = "https://app.asana.com/api/1.0/";

        public HttpClient CreateClient() {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
            client.BaseAddress = new Uri(base_url);
            return client;
        }


    }
}
