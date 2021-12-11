using Microsoft.Extensions.Logging;
using Mwm.MyQ.Client.Service.Storage;
using Mwm.MyQ.Client.Service.Store.Features.Shared.Effects;
using Mwm.MyQ.Domain;
using Mwm.MyQ.Application.Tsks.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mwm.MyQ.Client.Service.Store.Features.Tsks {
    public class TskLoadEffect : LoadEffect<Tsk> {

        public TskLoadEffect(ILogger<LoadEffect<Tsk>> logger, IEntityStorage entityStorage) : base(logger, entityStorage) { }

    }

    public class TskAddEffect : AddEffect<Tsk, TskAdd.Command> {

        public TskAddEffect(ILogger<AddEffect<Tsk, TskAdd.Command>> logger, IEntityStorage entityStorage) : base(logger, entityStorage) { }

    }

    public class TskUpdateEffect : UpdateEffect<Tsk, TskUpdate.Command> {

        public TskUpdateEffect(ILogger<UpdateEffect<Tsk, TskUpdate.Command>> logger, IEntityStorage entityStorage) : base(logger, entityStorage) { }

    }

    public class TskDeleteEffect : DeleteEffect<Tsk, TskDelete.Command> {

        public TskDeleteEffect(ILogger<DeleteEffect<Tsk, TskDelete.Command>> logger, IEntityStorage entityStorage) : base(logger, entityStorage) { }

    }

}
