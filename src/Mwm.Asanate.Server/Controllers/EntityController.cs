using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mwm.Asanate.Application.Interfaces.Persistance;
using Mwm.Asanate.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mwm.Asanate.Server.Controllers {
    public abstract class EntityController<TEntity> : ControllerBase where TEntity : IEntity {

        private readonly IRepository<TEntity> _repository;
        private readonly ILogger<EntityController<TEntity>> _logger;

        public EntityController(ILogger<EntityController<TEntity>> logger,
                                IRepository<TEntity> repository) {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<List<TEntity>> All() {
            return await _repository.GetAll().ToListAsync();
        }
    }


    [ApiController]
    [Route("[controller]")]
    public class ProjectController : EntityController<Project> {

        public ProjectController(ILogger<EntityController<Project>> logger, IRepository<Project> repository) : base(logger, repository) { }

    }

    [ApiController]
    [Route("[controller]")]
    public class CompanyController : EntityController<Company> {

        public CompanyController(ILogger<EntityController<Company>> logger, IRepository<Company> repository) : base(logger, repository) { }

    }

    [ApiController]
    [Route("[controller]")]
    public class InitiativeController : EntityController<Initiative> {

        public InitiativeController(ILogger<EntityController<Initiative>> logger, IRepository<Initiative> repository) : base(logger, repository) { }

    }

    [ApiController]
    [Route("[controller]")]
    public class TskController : EntityController<Tsk> {

        public TskController(ILogger<EntityController<Tsk>> logger, IRepository<Tsk> repository) : base(logger, repository) { }

    }


}
