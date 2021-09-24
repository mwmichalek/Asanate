using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mwm.Asanate.Application.Interfaces.Persistance;
using Mwm.Asanate.Application.Tsks.Commands;
using Mwm.Asanate.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mwm.Asanate.Server.Controllers {
    public abstract class EntityController<TEntity> : ControllerBase where TEntity : IEntity {

        protected readonly IRepository<TEntity> _repository;
        protected readonly ILogger<EntityController<TEntity>> _logger;
        protected readonly IMediator _mediator;

        public EntityController(ILogger<EntityController<TEntity>> logger,
                                IMediator mediator,
                                IRepository<TEntity> repository) {
            _repository = repository;
            _mediator = mediator;
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

        public ProjectController(ILogger<EntityController<Project>> logger, IMediator mediator, IRepository<Project> repository) : base(logger, mediator, repository) { }

    }

    [ApiController]
    [Route("[controller]")]
    public class CompanyController : EntityController<Company> {

        public CompanyController(ILogger<EntityController<Company>> logger, IMediator mediator, IRepository<Company> repository) : base(logger, mediator, repository) { }

    }

    [ApiController]
    [Route("[controller]")]
    public class InitiativeController : EntityController<Initiative> {

        public InitiativeController(ILogger<EntityController<Initiative>> logger, IMediator mediator, IRepository<Initiative> repository) : base(logger, mediator, repository) { }

    }

    [ApiController]
    [Route("[controller]")]
    public class TskController : EntityController<Tsk> {

        public TskController(ILogger<EntityController<Tsk>> logger, IMediator mediator, IRepository<Tsk> repository) : base(logger, mediator, repository) { }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTskCommand.Command command) {
            var result = await _mediator.Send(command);

            if (result.IsSuccess)
                return Ok();
            return BadRequest();
        }

    }

    //    [HttpGet("people/all")]
    //public ActionResult<IEnumerable<Person>> GetAll() {
    //        return new[]
    //        {
    //        new Person { Name = "Ana" },
    //        new Person { Name = "Felipe" },
    //        new Person { Name = "Emillia" }
    //    };

}
