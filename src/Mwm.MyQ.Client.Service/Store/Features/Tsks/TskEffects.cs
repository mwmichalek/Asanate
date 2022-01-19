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
using Mwm.MyQ.Client.Service.Store.Features.Shared.Actions;
using Fluxor;
using Mwm.MyQ.Client.Service.Store.State.Shared;
using Mwm.MyQ.Client.Service.Store.Features.Shared.Helpers;

namespace Mwm.MyQ.Client.Service.Store.Features.Tsks {
    public class TskLoadEffect : LoadEntityEffect<Tsk> {

        public TskLoadEffect(ILogger<LoadEntityEffect<Tsk>> logger, IEntityStorage entityStorage) : base(logger, entityStorage) { }

    }

    public class TskAddEffect : AddEntityEffect<Tsk, TskAdd.Command> {

        public TskAddEffect(ILogger<AddEntityEffect<Tsk, TskAdd.Command>> logger, IEntityStorage entityStorage) : base(logger, entityStorage) { }

    }

    public class TskUpdateEffect : UpdateEntityEffect<Tsk, TskUpdate.Command> {

        public TskUpdateEffect(ILogger<UpdateEntityEffect<Tsk, TskUpdate.Command>> logger, IEntityStorage entityStorage) : base(logger, entityStorage) { }

    }

    public class TskDeleteEffect : DeleteEntityEffect<Tsk, TskDelete.Command> {

        public TskDeleteEffect(ILogger<DeleteEntityEffect<Tsk, TskDelete.Command>> logger, IEntityStorage entityStorage) : base(logger, entityStorage) { }

    }

}
