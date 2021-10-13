using Microsoft.Extensions.Logging;
using Mwm.Asanate.Client.Blazor.Services.Storage;
using Mwm.Asanate.Client.Blazor.Store.Features.Shared.Effects;
using Mwm.Asanate.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mwm.Asanate.Client.Blazor.Store.Features.Tsks {
    public class TskLoadEffect : LoadEffect<Tsk> {

        public TskLoadEffect(ILogger<LoadEffect<Tsk>> logger, IEntityStorage entityStorage) : base(logger, entityStorage) { }

    }

    //public class LoadTsksEffect : LoadEffect<Tsk> {

    //    public LoadTsksEffect(ILogger<LoadEffect<Tsk>> logger, IEntityStorage entityStorage) : base(logger, entityStorage) { }

    //}

}
