using Mwm.MyQ.Client.Service.Models;
using Mwm.MyQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.MyQ.Client.Service.Store.Features.ModelFilters;

public class ModelFilterTypes {

    public static List<ModelFilter<TModel, TEntity>> DefaultModelFilters<TModel, TEntity>() where TModel : EntityModel<TEntity>
                                                                                            where TEntity : INamedEntity {
        return defaultModelFilters.Where(mf => mf is ModelFilter<TModel, TEntity>)
                                  .Cast<ModelFilter<TModel,TEntity>>()
                                  .ToList();
    }

    private static List<IModelFilter> defaultModelFilters = new List<IModelFilter> {
        new IsInFocusedTskModelFilter { IsApplied = false }
    };
}

public class IsInFocusedTskModelFilter : ModelFilter<TskModel, Tsk> {

    public override string Title => "Tsk == In Focus";

    public override IEnumerable<TskModel> Filter(IEnumerable<TskModel> models) {
        try {
            return models.Where(tm => tm.IsInFocus);
        } catch (Exception) {
            return models;
        }
    }

}

public class ByCompanyTskModelFilter : ModelFilter<TskModel, Tsk> {

    public override string Title => "Filter By Company";

    public override IEnumerable<TskModel> Filter(IEnumerable<TskModel> models) {
        try {
            return models.Where(tm => tm.IsInFocus);
        } catch (Exception) {
            return models;
        }
    }

}