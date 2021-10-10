using Microsoft.EntityFrameworkCore;
using Mwm.Asanate.Application.Interfaces.Persistance;
using Mwm.Asanate.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Mwm.Asanate.Persistance.Shared {

    public class Repository<T> : IRepository<T> where T : class, INamedEntity {
        protected readonly IDatabaseContext _database;

        public Repository(IDatabaseContext database) {
            _database = database;
        }

        public virtual IQueryable<T> GetAll() {
            return _database.Set<T>();
        }

        public virtual Task<List<T>> WhereAsync(Expression<Func<T, bool>> pred) {
            return _database.Set<T>().Where(pred).ToListAsync();
        }

        public Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> pred) {
            return _database.Set<T>().SingleOrDefaultAsync(pred);
        }

        public Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> pred) {
            return _database.Set<T>().FirstOrDefaultAsync(pred);
        }

        public T GetByName(string name) {
            return _database.Set<T>().SingleOrDefault(e => e.Name == name);
        }

        public Task<T> GetByNameAsync(string name) {
            return _database.Set<T>().SingleOrDefaultAsync(e => e.Name == name);
        }

        public T GetByGid(string gid) {
            return _database.Set<T>().SingleOrDefault(e => e.Gid == gid);
        }

        public Task<T> GetByGidAsync(string gid) {
            return _database.Set<T>().SingleOrDefaultAsync(e => e.Gid == gid);
        }

        public virtual T Get(int id) {
            return _database.Set<T>().SingleOrDefault(p => p.Id == id);
        }

        public virtual Task<T> GetAsync(int id) {
            return _database.Set<T>().SingleOrDefaultAsync(p => p.Id == id);
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

        public virtual Task SaveAsync() {
            return _database.SaveAsync();
        }

       
    }
}
