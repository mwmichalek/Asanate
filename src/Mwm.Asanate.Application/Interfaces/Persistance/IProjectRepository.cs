using Mwm.Asanate.Application.Interfaces.Persistance;
using Mwm.Asanate.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Persistance {
    public interface IProjectRepository : IRepository<Project> {

        Project GetOrCreate(uint projectId);

        List<Project> GetStarredProjects();
    }
}
