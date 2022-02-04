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

public class TskFilterModelEffect : FilterModelEffect<TskModel, Tsk> {
    public TskFilterModelEffect(ILogger<FilterModelEffect<TskModel, Tsk>> logger, IState<ModelState<TskModel, Tsk>> modelState) : 
        base(logger, modelState) {
    }
}
