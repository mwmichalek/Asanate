using Microsoft.Extensions.Logging;
using Mwm.MyQ.Client.Service.Storage;
using Mwm.MyQ.Client.Service.Store.Features.Shared.Effects;
using Mwm.MyQ.Domain;
using Mwm.MyQ.Application.Initiatives.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mwm.MyQ.Client.Service.Store.Features.Tsks {
    public class InitiativeLoadEffect : LoadEntityEffect<Initiative> {

        public InitiativeLoadEffect(ILogger<LoadEntityEffect<Initiative>> logger, IEntityStorage entityStorage) : base(logger, entityStorage) { }

    }

    public class InitiativeAddEffect : AddEntityEffect<Initiative, InitiativeAdd.Command> {

        public InitiativeAddEffect(ILogger<AddEntityEffect<Initiative, InitiativeAdd.Command>> logger, IEntityStorage entityStorage) : base(logger, entityStorage) { }

    }

    public class InitiativeUpdateEffect : UpdateEntityEffect<Initiative, InitiativeUpdate.Command> {

        public InitiativeUpdateEffect(ILogger<UpdateEntityEffect<Initiative, InitiativeUpdate.Command>> logger, IEntityStorage entityStorage) : base(logger, entityStorage) { }

    }

    public class InitiativeDeleteEffect : DeleteEntityEffect<Initiative, InitiativeDelete.Command> {

        public InitiativeDeleteEffect(ILogger<DeleteEntityEffect<Initiative, InitiativeDelete.Command>> logger, IEntityStorage entityStorage) : base(logger, entityStorage) { }

    }

}
