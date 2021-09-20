using Microsoft.EntityFrameworkCore;
using Mwm.Asanate.Application.Interfaces.Persistance;
using Mwm.Asanate.Domain;
using Mwm.Asanate.Persistance.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.Asanate.Persistance.Members {
    public class ProjectRepository : Repository<Project>, IProjectRepository {
        public ProjectRepository(IDatabaseContext databaseContext)
            : base(databaseContext) { }

        public override IQueryable<Project> GetAll() {
            return _database.Projects.Include(t => t.Company);
        }

        public List<Project> GetStarredProjects() {
            throw new NotImplementedException();
        }
    }
}
