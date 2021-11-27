using Microsoft.EntityFrameworkCore;
using Mwm.MyQ.Domain;
using Mwm.MyQ.Persistance.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.MyQ.Persistance {
    public class InitiativeRepository : Repository<Initiative> {

        public InitiativeRepository(IDatabaseContext databaseContext) : base(databaseContext) { }

        // Overriding to include Status
        public override IQueryable<Initiative> GetAll() {
            return _database.Initiatives.Include(t => t.Project.Company);
        }

        public override Initiative Get(int id) {
            return _database.Set<Initiative>()
                .Include(t => t.Project.Company)
                .SingleOrDefault(p => p.Id == id);
        }
    }
}
