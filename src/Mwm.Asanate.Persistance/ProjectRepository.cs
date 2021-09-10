
using Application.Interfaces.Persistance;
using Microsoft.EntityFrameworkCore;
using Mwm.Asanate.Domain;
using Mwm.Asanate.Persistance.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Olive.LRP.Persistance.Members {
    public class ProjectRepository : Repository<Project>, IProjectRepository {
        public ProjectRepository(IDatabaseContext database)
            : base(database) { }

        public Project GetOrCreate(uint projectId) {
            var existingProject = _database.Set<Project>() //.Include(m => m.CreditGrants)
                                          .SingleOrDefault(p => p.Id == projectId);

            if (existingProject == null) {
                existingProject = new Project {
                    Id = projectId
                };
                Add(existingProject);
                Save();
            }

            return existingProject;
        }

        public List<Project> GetStarredProjects() {
            throw new NotImplementedException();
        }
    }
}
