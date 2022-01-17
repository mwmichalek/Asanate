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
    public class ProjectLoadEffect : LoadEntityEffect<Project> {

        public ProjectLoadEffect(ILogger<LoadEntityEffect<Project>> logger, IEntityStorage entityStorage) : base(logger, entityStorage) { }

    }

}
