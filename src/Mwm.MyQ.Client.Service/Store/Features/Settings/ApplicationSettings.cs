using Mwm.MyQ.Client.Service.Models;
using Mwm.MyQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.MyQ.Client.Service.Store.Features.Settings;

public interface IApplicationSetting { }

public interface IPrimativeApplicationSetting : IApplicationSetting { }

public interface IObjectApplicationSetting : IApplicationSetting { }

public abstract class ObjectApplicationSetting<TClass> : IObjectApplicationSetting where TClass : class {

    public TClass PreviousValue { get; set; }

    public TClass CurrentValue { get; set; }

}

public abstract class PrimativeApplicationSetting<TPrimative> : IPrimativeApplicationSetting where TPrimative : struct {

    public TPrimative PreviousValue { get; set; }

    public TPrimative CurrentValue { get; set; }

}

public interface IModelFilter<TNamedEntity> where TNamedEntity : INamedEntity {
    IEnumerable<EntityModel<TNamedEntity>> Filter(IEnumerable<EntityModel<TNamedEntity>> entities);

    bool IsApplied { get; }

    string Title { get; }
}



public interface IPrimativeModelFilter<TNamedEntity, TPrimative> : IModelFilter<TNamedEntity> where TNamedEntity : INamedEntity
                                                                                                where TPrimative : struct {
    TPrimative FilterValue { get; set; }
}

public interface IObjectModelFilter<TNamedEntity, TClass> : IModelFilter<TNamedEntity> where TNamedEntity : INamedEntity
                                                                                         where TClass : class { 
    TClass FilterValue { get; set; }
}

public abstract class ModelFilter<TNamedEntity> : IModelFilter<TNamedEntity> where TNamedEntity : INamedEntity {

    public abstract IEnumerable<EntityModel<TNamedEntity>> Filter(IEnumerable<EntityModel<TNamedEntity>> entities);

    public bool IsApplied { get; set; } = false;

    public virtual string Title => "Name == Something";

}


public abstract class PrimativeModelFilter<TNamedEntity, TPrimative> : ModelFilter<TNamedEntity>, 
                                                                       IPrimativeModelFilter<TNamedEntity, TPrimative> where TNamedEntity : INamedEntity 
                                                                                                                       where TPrimative : struct {
    public TPrimative FilterValue { get; set; }

}

public abstract class ObjectModelFilter<TNamedEntity, TClass> : ModelFilter<TNamedEntity>,
                                                                 IObjectModelFilter<TNamedEntity, TClass> where TNamedEntity : INamedEntity 
                                                                                                          where TClass : class {
    public TClass FilterValue { get; set; }                                                                                
}

