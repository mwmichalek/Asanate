using Microsoft.Extensions.Logging;
using Mwm.Asanate.Client.Service.Storage;
using Mwm.Asanate.Client.Service.Store.Features.Shared.Effects;
using Mwm.Asanate.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mwm.Asanate.Client.Service.Store.Features.Tsks {
    public class LoadInitiativeEffect : LoadEffect<Initiative> {

        public LoadInitiativeEffect(ILogger<LoadEffect<Initiative>> logger, IEntityStorage entityStorage) : base(logger, entityStorage) { }

    }

}
