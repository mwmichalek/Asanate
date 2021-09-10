using Mwm.Asanate.Application.Interfaces.Persistance;
using Mwm.Asanate.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Mwm.Asanate.Persistance.Shared {
    public class Repository<T> : IRepository<T> where T : class, IEntity {
        protected readonly IDatabaseContext _database;

        public Repository(IDatabaseContext database) {
            _database = database;
        }

        public virtual IQueryable<T> GetAll() {
            return _database.Set<T>();
        }

        public virtual T Get(uint id) {
            return _database.Set<T>()
                .SingleOrDefault(p => p.Id == id);
        }

        public virtual void Add(T entity) {
            _database.Set<T>().Add(entity);
        }

        public virtual void Remove(T entity) {
            _database.Set<T>().Remove(entity);
        }

        public virtual void Save() {
            _database.Save();
        }
    }
}
