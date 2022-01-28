using Mwm.MyQ.Client.Service.Components;
using Mwm.MyQ.Client.Service.Models;
using Mwm.MyQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.MyQ.Client.Service.Store.Features.Settings;

public class ApplicationSettingTypes {

    public static List<IApplicationSetting> DefaultSettings = new List<IApplicationSetting> {
        new IsInFocusOnlyFlag(),
        new IsGroupedByCompanyFlag { CurrentValue = true },
        new IsActionStatusOnlyFlag(),
        new IsInFocusedTskModelFilter { IsApplied = false }
    };
}

public class IsInFocusOnlyFlag : PrimativeApplicationSetting<bool> { }

public class IsGroupedByCompanyFlag : PrimativeApplicationSetting<bool> { }

public class IsActionStatusOnlyFlag : PrimativeApplicationSetting<bool> { }


public class IsInFocusedTskModelFilter : ModelFilter<TskModel, Tsk> {

    public override string Title => "Tsk == In Focus";

    public override IEnumerable<TskModel> Filter(IEnumerable<TskModel> models) {
        try {
            return models.Cast<TskModel>().Where(tm => tm.IsInFocus);
        } catch (Exception) {
            return models;
        }
    }
       
}
