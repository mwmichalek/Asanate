using Mwm.Asanate.Application.Interfaces.Persistance;
using Mwm.Asanate.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Mwm.Asanate.Persistance.Shared {

    public class Repository<T> : IRepository<T> where T : class, INamedEntity {
        protected readonly IDatabaseContext _database;

        public Repository(IDatabaseContext database) {
            _database = database;
        }

        public virtual IQueryable<T> GetAll() {
            return _database.Set<T>();
        }

        public T GetByName(string name) {
            return GetAll().SingleOrDefault(e => e.Name == name);
        }

        public T GetByGid(string gid) {
            return GetAll().SingleOrDefault(e => e.Gid == gid);
        }

        public virtual T Get(int id) {
            return _database.Set<T>()
                .SingleOrDefault(p => p.Id == id);
        }

        public virtual T Add(T entity) {
            return _database.Set<T>().Add(entity).Entity;
        }

        public virtual void Remove(T entity) {
            _database.Set<T>().Remove(entity);
        }

        public virtual void Save() {
            _database.Save();
        }

       
    }
}
