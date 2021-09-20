using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.Asanate.Application.Interfaces.Persistance {
    public interface IRepository<T> {
        IQueryable<T> GetAll();

        T GetByGid(string gid);

        T GetByName(string name);

        T Get(int id);

        T Add(T entity);

        void Remove(T entity);

        void Save();
    }
}
