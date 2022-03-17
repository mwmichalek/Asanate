using Microsoft.EntityFrameworkCore;
using Mwm.MyQ.Domain;
using Mwm.MyQ.Persistance.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.MyQ.Persistance {
    public class TskRepository : Repository<Tsk> {

        public TskRepository(IDatabaseContext databaseContext) : base(databaseContext) { }

        // Overriding to include Status
        public override IQueryable<Tsk> GetAll() {
            return _database.Tsks.Include(t => t.Activities);
        }

        //public override Task<List<Tsk>> WhereAsync(Expression<Func<Tsk, bool>> pred) {
        //    return base.WhereAsync(pred).Include();
        //}

        public override Tsk Get(int id) {
            return _database.Set<Tsk>()
                .Include(t => t.Activities)
                .SingleOrDefault(p => p.Id == id);
        }

    }
}
