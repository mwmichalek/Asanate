using Fluxor;
using Microsoft.Extensions.Logging;
using Mwm.MyQ.Client.Service.Models;
using Mwm.MyQ.Client.Service.Store.Features.Shared.Effects;
using Mwm.MyQ.Client.Service.Store.State.Shared;
using Mwm.MyQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.MyQ.Client.Service.Store.Features.TskModels;

public class TskLoadTriggersLoadTskModelEffect : EntityLoadTriggersLoadModelEffect<TskModel, Tsk, Tsk> {
    public TskLoadTriggersLoadTskModelEffect(ILogger<EntityLoadTriggersLoadModelEffect<TskModel, Tsk, Tsk>> logger, IState<EntityState<Tsk>> entityState) : base(logger, entityState) {
    }
}

public class InitiativeLoadTriggersLoadTskModelEffect : EntityLoadTriggersLoadModelEffect<TskModel, Tsk, Initiative> {
    public InitiativeLoadTriggersLoadTskModelEffect(ILogger<EntityLoadTriggersLoadModelEffect<TskModel, Tsk, Initiative>> logger, IState<EntityState<Tsk>> entityState) : base(logger, entityState) {
    }
}

public class ProjectLoadTriggersLoadTskModelEffect : EntityLoadTriggersLoadModelEffect<TskModel, Tsk, Project> {
    public ProjectLoadTriggersLoadTskModelEffect(ILogger<EntityLoadTriggersLoadModelEffect<TskModel, Tsk, Project>> logger, IState<EntityState<Tsk>> entityState) : base(logger, entityState) {
    }
}

public class CompanyLoadTriggersLoadTskModelEffect : EntityLoadTriggersLoadModelEffect<TskModel, Tsk, Company> {
    public CompanyLoadTriggersLoadTskModelEffect(ILogger<EntityLoadTriggersLoadModelEffect<TskModel, Tsk, Company>> logger, IState<EntityState<Tsk>> entityState) : base(logger, entityState) {
    }
}

public class TskAddTriggersLoadTskModelEffect : EntityAddTriggersLoadModelEffect<TskModel, Tsk, Tsk> {
    public TskAddTriggersLoadTskModelEffect(ILogger<EntityAddTriggersLoadModelEffect<TskModel, Tsk, Tsk>> logger, IState<EntityState<Tsk>> entityState) : base(logger, entityState) {
    }
}

public class TskUpdateTriggersLoadTskModelEffect : EntityUpdateTriggersLoadModelEffect<TskModel, Tsk, Tsk> {
    public TskUpdateTriggersLoadTskModelEffect(ILogger<EntityUpdateTriggersLoadModelEffect<TskModel, Tsk, Tsk>> logger, IState<EntityState<Tsk>> entityState) : base(logger, entityState) {
    }
}

public class TskDeleteTriggersLoadTskModelEffect : EntityDeleteTriggersLoadModelEffect<TskModel, Tsk, Tsk> {
    public TskDeleteTriggersLoadTskModelEffect(ILogger<EntityDeleteTriggersLoadModelEffect<TskModel, Tsk, Tsk>> logger, IState<EntityState<Tsk>> entityState) : base(logger, entityState) {
    }
}