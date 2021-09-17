using Microsoft.EntityFrameworkCore;
using Mwm.Asanate.Domain;
using Mwm.Asanate.Persistance.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.Asanate.Persistance {
    public class TskRepository : Repository<Tsk> {

        public TskRepository(IDatabaseContext databaseContext) : base(databaseContext) { }

        // Overriding to include Status
        public override IQueryable<Tsk> GetAll() {
            return _database.Tsks.Include(t => t.Initiative.Project.Company);
        }
    }
}
