using Mwm.MyQ.Application.Interfaces.Persistance;
using Mwm.MyQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.MyQ.Application.Interfaces.Persistance {
    public interface IProjectRepository : IRepository<Project> {

        List<Project> GetStarredProjects();
    }
}
