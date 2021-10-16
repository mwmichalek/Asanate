using Microsoft.Extensions.Logging;
using Mwm.Asanate.Client.Service.Storage;
using Mwm.Asanate.Client.Service.Store.Features.Shared.Effects;
using Mwm.Asanate.Domain;
using Mwm.Asanate.Application.Tsks.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mwm.Asanate.Client.Service.Store.Features.Tsks {
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
