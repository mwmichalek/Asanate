using Mwm.Asanate.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mwm.Asanate.Client.Blazor.Services.Storage {

    public interface IEntityStorage {

        List<TEntity> GetAll<TEntity>() where TEntity : INamedEntity;
    
    }
    public class FileEntityStorage : IEntityStorage {

        public List<TEntity> GetAll<TEntity>() where TEntity : INamedEntity {
            return null;
        }
    }
}
