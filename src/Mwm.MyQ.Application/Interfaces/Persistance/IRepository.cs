using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.MyQ.Application.Interfaces.Persistance {
    public interface IRepository<T> {
        IQueryable<T> GetAll();

        Task<List<T>> WhereAsync(Expression<Func<T, bool>> pred);

        Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> pred);

        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> pred);

        T GetByGid(string gid);

        Task<T> GetByGidAsync(string gid);

        T GetByName(string name);

        Task<T> GetByNameAsync(string name);

        T Get(int id);

        Task<T> GetAsync(int id);

        T Add(T entity);

        void Remove(T entity);

        void Save();

        Task SaveAsync();
    }
}
