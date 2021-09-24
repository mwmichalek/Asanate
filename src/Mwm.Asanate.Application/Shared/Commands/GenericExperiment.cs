using FluentResults;
using MediatR;
using Mwm.Asanate.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.Asanate.Application.Shared.Commands {



    public abstract class CommandType { }

    public abstract class PostCommandType : CommandType { }

    public class Add : PostCommandType { }

    public class Update : PostCommandType { }



    public interface ICommandType { }

    public interface IPostCommandType : ICommandType { }

    public interface IAdd : IPostCommandType { }

    public interface IUpdate : IPostCommandType { }


    public class EntityCommand<TEntity, TCommandType> : IRequest<Result> where TEntity : NamedEntity where TCommandType : CommandType {
    }

    public interface IEntityCommand<TEntity, TCommandType> : IRequest<Result> where TEntity : NamedEntity where TCommandType : CommandType {
    }

    public interface IEntityAddCommand<TEntity, Add> : IRequest<Result> where TEntity : NamedEntity {
    }

    public interface IEntityUpdateCommand<TEntity, Update> : IRequest<Result> where TEntity : NamedEntity {
    }
}
