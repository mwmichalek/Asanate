﻿using Microsoft.EntityFrameworkCore;
using Mwm.MyQ.Application.Interfaces.Persistance;
using Mwm.MyQ.Domain;
using Mwm.MyQ.Persistance.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.MyQ.Persistance.Members {
    public class ProjectRepository : Repository<Project>, IProjectRepository {
        public ProjectRepository(IDatabaseContext databaseContext)
            : base(databaseContext) { }

        public override IQueryable<Project> GetAll() {
            return _database.Projects.Include(t => t.Company);
        }

        public override Project Get(int id) {
            return _database.Set<Project>()
                .Include(t => t.Company)
                .SingleOrDefault(p => p.Id == id);
        }

        public List<Project> GetStarredProjects() {
            throw new NotImplementedException();
        }
    }
}
