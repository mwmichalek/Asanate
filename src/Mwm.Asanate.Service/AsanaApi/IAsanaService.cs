using Mwm.Asanate.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.Asanate.Service.AsanaApi {
    public interface IAsanaService {

        Task<List<TEntity>> GetAll<TEntity>() where TEntity : AsanateEntity;

    }
}
