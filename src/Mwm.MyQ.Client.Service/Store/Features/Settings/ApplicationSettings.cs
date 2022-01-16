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

public interface IEntityFilter<TNamedEntity> where TNamedEntity : INamedEntity {
    bool Filter(TNamedEntity entity);

    bool IsApplied { get; }

    string Title { get; }
}



public interface IPrimativeEntityFilter<TNamedEntity, TPrimative> : IEntityFilter<TNamedEntity> where TNamedEntity : INamedEntity
                                                                                                where TPrimative : struct {
    TPrimative FilterValue { get; set; }
}

public interface IObjectEntityFilter<TNamedEntity, TClass> : IEntityFilter<TNamedEntity> where TNamedEntity : INamedEntity
                                                                                         where TClass : class { 
    TClass FilterValue { get; set; }
}

public abstract class EntityFilter<TNamedEntity> : IEntityFilter<TNamedEntity> where TNamedEntity : INamedEntity {
    public abstract bool Filter(TNamedEntity namedEntity);

    public bool IsApplied { get; set; } = false;

    public virtual string Title => "Name == Something";

}


public abstract class PrimativeEntityFilter<TNamedEntity, TPrimative> : EntityFilter<TNamedEntity>, 
                                                                        IPrimativeEntityFilter<TNamedEntity, TPrimative> where TNamedEntity : INamedEntity 
                                                                                                                         where TPrimative : struct {
    public TPrimative FilterValue { get; set; }

}

public abstract class ObjectEntityFilter<TNamedEntity, TClass> : EntityFilter<TNamedEntity>,
                                                                 IObjectEntityFilter<TNamedEntity, TClass> where TNamedEntity : INamedEntity 
                                                                                                            where TClass : class {
    public TClass FilterValue { get; set; }                                                                                
}

