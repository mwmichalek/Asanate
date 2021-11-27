using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mwm.MyQ.Server.Controllers {


    [ApiController]
    [Route("[controller]")]
    public class WebhookController : ControllerBase {
       
        private readonly ILogger<WebhookController> _logger;

        // The Web API will only accept tokens 1) for users, and 2) having the "access_as_user" scope for this API
        static readonly string[] scopeRequiredByApi = new string[] { "access_as_user" };

        public WebhookController(ILogger<WebhookController> logger) {
            _logger = logger;
        }

        [HttpPost]
        public async Task HandleUpdates() {
            if (Request.Headers.TryGetValue("X-Hook-Secret", out StringValues secrets))
                await HandleWebhookAuth(secrets.FirstOrDefault());


            //HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);

        }

        private Task HandleWebhookAuth(string secret) {
            return Task.CompletedTask;
        }
    }
}
