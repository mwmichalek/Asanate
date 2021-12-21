using Microsoft.EntityFrameworkCore;
using Mwm.MyQ.Domain;
using Mwm.MyQ.Persistance.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.MyQ.Persistance {
    public class _TskRepository : Repository<Tsk> {

        public _TskRepository(IDatabaseContext databaseContext) : base(databaseContext) { }

        // Overriding to include Status
        public override IQueryable<Tsk> GetAll() {
            return _database.Tsks; //.Include(t => t.Initiative.Project.Company);
        }

        public override Tsk Get(int id) {
            return _database.Set<Tsk>()
                //.Include(t => t.Initiative.Project.Company)
                .SingleOrDefault(p => p.Id == id);
        }

    }
}
