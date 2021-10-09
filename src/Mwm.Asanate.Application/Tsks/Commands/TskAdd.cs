using FluentResults;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using Mwm.Asanate.Application.Interfaces.Persistance;
using Mwm.Asanate.Application.Shared.Commands;
using Mwm.Asanate.Application.Utils;
using Mwm.Asanate.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Mwm.Asanate.Application.Tsks.Commands {


    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger) {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next) {
            _logger.LogInformation($">>>>>>>  {typeof(TRequest).Name}");
            var response = await next();
            _logger.LogInformation($"<<<<<<<<  {typeof(TResponse).Name}");

            return response;
        }
    }


    // https://github.com/jbogard/MediatR.Extensions.Microsoft.DependencyInjection

    public class EntityCommandPostProcessor<TRequest, TResult> : IRequestPostProcessor<TRequest, TResult> where TRequest : ICommand {

        private IMediator _mediator;

        public EntityCommandPostProcessor(IMediator mediator) {
            _mediator = mediator;
        }

        public Task Process(TRequest command, TResult response, CancellationToken cancellationToken) {


            if (command is TskAdd.Command tskAdd)
                Console.WriteLine("NUTS!");
                //_mediator.Send()


            return Task.CompletedTask;
        }
    }


    //public class Success<IAddEntityCommand<Tsk>> where TEntity : INamedEntity {
    //}


    //public class SuccessEventHandler



//    public class Pong2 : INotificationHandler<Ping> {
//        public Task Handle(Ping notification, CancellationToken cancellationToken) {
//            Debug.WriteLine("Pong 2");
//            return Task.CompletedTask;
//        }
//    }
//    Finally, publish your message via the mediator:

//await mediator.Publish(new Ping());


//    Open generics
//If you have an open generic not listed above, you'll need to register it explicitly. For example, if you have an open generic request handler, register the open generic types explicitly:

//services.AddTransient(typeof(IRequestHandler<,>), typeof(GenericHandlerBase<,>));
//This won't work with generic constraints, so you're better off creating an abstract base class and concrete closed generic classes that fill in the right types.





    public class TskAdd {

        public class Command : IAddEntityCommand<Tsk> {

            public string Name { get; set; }

            public string? ExternalId { get; set; } 

            public Status Status { get; set; } 

            public bool? IsArchived { get; set; }

            public bool? IsCompleted { get; set; }

            public int? DurationEstimate { get; set; }

            public int? DuractionCompleted { get; set; }

            public int? PercentageCompleted { get; set; }

            public string? Notes { get; set; }

            public DateTime? DueDate { get; set; }

            public DateTime? StartDate { get; set; }

            public DateTime? StartedDate { get; set; }

            public DateTime? CompletedDate { get; set; }

            public int? AssignedToId { get; set; }

            public int? CreatedById { get; set; } 

            public int? ModifiedById { get; set; } 

            public int? InitiativeId { get; set; }

        }

        public class Handler : IRequestHandler<Command, Result<int>> {

            private ILogger<Handler> _logger;

            private readonly IRepository<User> _userRepository;
            private readonly IRepository<Initiative> _initiativeRepository;
            private readonly IRepository<Tsk> _tskRepository;

            public Handler(ILogger<Handler> logger,
                           IRepository<User> userRepository,
                           IRepository<Initiative> initiativeRepository,
                           IRepository<Tsk> tskRepository) {
                _logger = logger;
                _userRepository = userRepository;
                _initiativeRepository = initiativeRepository;
                _tskRepository = tskRepository;
            }

            public async Task<Result<int>> Handle(Command command, CancellationToken cancellationToken) {

                if (string.IsNullOrEmpty(command.Name))
                    return Result.Fail("Tsk Name can't be null.");

                var initiativeResult = await FindOrCreateInitiative(command);

                if (initiativeResult.IsFailed)
                    return initiativeResult.ToResult();
                command.InitiativeId = initiativeResult.Value;
                var initiativeSuccess = initiativeResult.Successes.FirstOrDefault();

                var tskResult = await CreateTsk(command);
                tskResult.Successes.Add(initiativeSuccess);

                return tskResult;
            }

            private async Task<Result<int>> FindOrCreateInitiative(Command command) {
                try {
                    if (command.InitiativeId.HasValue) {
                        var initiative = await _initiativeRepository.GetAsync(command.InitiativeId.Value);
                        _logger.LogInformation($"Initiative Found: {initiative.Name}");
                        return Result.Ok(initiative.Id).WithSuccess(initiative.ToSuccess(ResultAction.Find));
                    } else {
                        // Default "Generic" - "Triage"
                        var initiative = await _initiativeRepository.SingleOrDefaultAsync(i => i.Name == Initiative.DefaultInitiativeName &&
                                                                                               i.Project.Name == Project.DefaultProjectName);
                        _logger.LogInformation($"Initiative Found: {initiative.Name}");
                        return Result.Ok(initiative.Id).WithSuccess(initiative.ToSuccess(ResultAction.Find));
                    }
                } catch (Exception ex) {
                    _logger.LogError($"Initiative Add/Find Failure: {ex}");
                    return Result.Fail(new Error("Unable to create Initiative").CausedBy(ex));
                }
            }


            private async Task<Result<int>> CreateTsk(Command command) {
                try {
                    var tsk = new Tsk {
                        Name = command.Name,
                        ExternalId = command.ExternalId,
                        Notes = command.Notes,
                        CompletedDate = command.CompletedDate,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        DueDate = command.DueDate,
                        StartedDate = command.StartedDate,
                        Status = command.Status,
                        InitiativeId = command.InitiativeId.Value,
                        AssignedToId = User.MeId
                    };
                    if (command.AssignedToId.HasValue) tsk.AssignedToId = command.AssignedToId.Value;
                    if (command.IsArchived.HasValue) tsk.IsArchived = command.IsArchived.Value;
                    _tskRepository.Add(tsk);
                    await _tskRepository.SaveAsync();
                    _logger.LogInformation($"Tsk Added: {tsk.Name}");
                    return Result.Ok(tsk.Id).WithSuccess(tsk.ToSuccess(ResultAction.Add));
                } catch (Exception ex) {
                    _logger.LogError($"Tsk Addition Failure: {ex}");
                    return Result.Fail(new Error("Unable to create Tsk").CausedBy(ex));
                }
            }
        }
    }
}
