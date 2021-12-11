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
    public class InitiativeLoadEffect : LoadEffect<Initiative> {

        public InitiativeLoadEffect(ILogger<LoadEffect<Initiative>> logger, IEntityStorage entityStorage) : base(logger, entityStorage) { }

    }

    public class InitiativeAddEffect : AddEffect<Initiative, InitiativeAdd.Command> {

        public InitiativeAddEffect(ILogger<AddEffect<Initiative, InitiativeAdd.Command>> logger, IEntityStorage entityStorage) : base(logger, entityStorage) { }

    }

    public class InitiativeUpdateEffect : UpdateEffect<Initiative, InitiativeUpdate.Command> {

        public InitiativeUpdateEffect(ILogger<UpdateEffect<Initiative, InitiativeUpdate.Command>> logger, IEntityStorage entityStorage) : base(logger, entityStorage) { }

    }

    public class InitiativeDeleteEffect : DeleteEffect<Initiative, InitiativeDelete.Command> {

        public InitiativeDeleteEffect(ILogger<DeleteEffect<Initiative, InitiativeDelete.Command>> logger, IEntityStorage entityStorage) : base(logger, entityStorage) { }

    }

}
