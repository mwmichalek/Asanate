﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mwm.Asanate.Application.Interfaces.Persistance;
using Mwm.Asanate.Application.Shared.Commands;
using Mwm.Asanate.Application.Tsks.Commands;
using Mwm.Asanate.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Mwm.Asanate.Server.Controllers {

    public interface IEntityController<TEntity> where TEntity : INamedEntity { }

    [Route("api/[controller]")]
    [ApiController]
    public class EntityController<TEntity, TAddEntityCommand, TUpdateEntityCommand, TDeleteEntityCommand> : 
                          ControllerBase, IEntityController<TEntity>
                          where TEntity : NamedEntity 
                          where TAddEntityCommand : IAddEntityCommand<TEntity>
                          where TUpdateEntityCommand : IUpdateEntityCommand<TEntity>
                          where TDeleteEntityCommand : IDeleteEntityCommand<TEntity> {

        protected readonly IRepository<TEntity> _repository;
        protected readonly ILogger<IEntityController<TEntity>> _logger;
        protected readonly IMediator _mediator;

        public EntityController(ILogger<IEntityController<TEntity>> logger,
                                IMediator mediator,
                                IRepository<TEntity> repository) {
            _repository = repository;
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<List<TEntity>> All() {
            var sw = Stopwatch.StartNew();
            var result = await _repository.GetAll().ToListAsync();
            _logger.LogInformation($"Loaded {result.Count} {typeof(TEntity).Name} is {sw.ElapsedMilliseconds} ms");
            return result;
        }

        [HttpGet("Get/{id}")]
        public async Task<TEntity> Get(int id) {
            var sw = Stopwatch.StartNew();
            var result = await _repository.GetAll().SingleOrDefaultAsync(e => e.Id == id);
            _logger.LogInformation($"Loaded {result.Name} {typeof(TEntity).Name} is {sw.ElapsedMilliseconds} ms");
            return result;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(TAddEntityCommand command) {
            var result = await _mediator.Send(command);

            if (result.IsSuccess)
                return Ok();
            return BadRequest();
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update(TUpdateEntityCommand command) {
            var result = await _mediator.Send(command);

            if (result.IsSuccess)
                return Ok();
            return BadRequest();
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete(TDeleteEntityCommand command) {
            var result = await _mediator.Send(command);

            if (result.IsSuccess)
                return Ok();
            return BadRequest();
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : EntityController<Project, 
                                                      AddNotSupportedEntityCommand<Project>,
                                                      UpdateNotSupportedEntityCommand<Project>,
                                                      DeleteNotSupportedEntityCommand<Project>> {

        public ProjectController(ILogger<IEntityController<Project>> logger, IMediator mediator, IRepository<Project> repository) : base(logger, mediator, repository) { }

    }

    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : EntityController<Company,
                                                      AddNotSupportedEntityCommand<Company>,
                                                      UpdateNotSupportedEntityCommand<Company>,
                                                      DeleteNotSupportedEntityCommand<Company>> {

        public CompanyController(ILogger<IEntityController<Company>> logger, IMediator mediator, IRepository<Company> repository) : base(logger, mediator, repository) { }

    }

    [Route("api/[controller]")]
    [ApiController]
    public class InitiativeController : EntityController<Initiative, 
                                                         AddNotSupportedEntityCommand<Initiative>,
                                                         UpdateNotSupportedEntityCommand<Initiative>,
                                                         DeleteNotSupportedEntityCommand<Initiative>> {


        public InitiativeController(ILogger<IEntityController<Initiative>> logger, IMediator mediator, IRepository<Initiative> repository) : base(logger, mediator, repository) { }

    }

    [Route("api/[controller]")]
    [ApiController]
    public class TskController : EntityController<Tsk, 
                                                  TskAdd.Command,
                                                  UpdateNotSupportedEntityCommand<Tsk>,
                                                  DeleteNotSupportedEntityCommand<Tsk>> {

        public TskController(ILogger<IEntityController<Tsk>> logger, IMediator mediator, IRepository<Tsk> repository) : base(logger, mediator, repository) { }

    }
}
