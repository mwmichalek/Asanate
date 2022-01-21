using Mwm.MyQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.MyQ.Client.Service.Models;

//public interface IEntityModel<TNamedEntity> where TNamedEntity : INamedEntity {

//    public int Id { get; }

//    public string Name { get; }

//}

public abstract class EntityModel<TNamedEntity> /*: IEntityModel<TNamedEntity>*/ where TNamedEntity : INamedEntity {

    public int Id { get; set; }

    public string Name { get; set; }

}
