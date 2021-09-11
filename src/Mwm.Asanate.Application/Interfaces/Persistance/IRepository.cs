using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.Asanate.Application.Interfaces.Persistance {
    public interface IRepository<T> {
        IQueryable<T> GetAll();

        T GetByName(string name);

        T Get(uint id);

        void Add(T entity);

        void Remove(T entity);

        void Save();
    }
}
