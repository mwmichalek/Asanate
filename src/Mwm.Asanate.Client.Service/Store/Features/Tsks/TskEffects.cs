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
    public class TskLoadEffect : LoadEffect<Tsk> {

        public TskLoadEffect(ILogger<LoadEffect<Tsk>> logger, IEntityStorage entityStorage) : base(logger, entityStorage) { }

    }

    public class TskAddEffect : AddEffect<Tsk> {

        public TskAddEffect(ILogger<AddEffect<Tsk>> logger, IEntityStorage entityStorage) : base(logger, entityStorage) { }

    }

    public class TskUpdateEffect : UpdateEffect<Tsk> {

        public TskUpdateEffect(ILogger<UpdateEffect<Tsk>> logger, IEntityStorage entityStorage) : base(logger, entityStorage) { }

    }

}
