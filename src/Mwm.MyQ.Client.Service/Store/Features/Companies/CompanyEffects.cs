using Microsoft.Extensions.Logging;
using Mwm.MyQ.Client.Service.Storage;
using Mwm.MyQ.Client.Service.Store.Features.Shared.Effects;
using Mwm.MyQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mwm.MyQ.Client.Service.Store.Features.Tsks {
    public class CompanyLoadEffect : LoadEntityEffect<Company> {

        public CompanyLoadEffect(ILogger<LoadEntityEffect<Company>> logger, IEntityStorage entityStorage) : base(logger, entityStorage) { }

    }

}
