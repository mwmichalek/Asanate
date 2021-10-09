using FluentResults;
using MediatR;
using Mwm.Asanate.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.Asanate.Application.Shared.Commands {

    public interface IEntityCommand<TEntity> where TEntity : INamedEntity { }

    public interface IPostEntityCommand<TEntity> : IEntityCommand<TEntity>, IRequest<Result> where TEntity : INamedEntity { }

    public interface IAddEntityCommand<TEntity> : IPostEntityCommand<TEntity> where TEntity : INamedEntity { }

    public interface IUpdateEntityCommand<TEntity> : IPostEntityCommand<TEntity> where TEntity : INamedEntity { }

    public interface IDeleteEntityCommand<TEntity> : IPostEntityCommand<TEntity> where TEntity : INamedEntity { }

    public interface INotSupportedEntityCommand { }

    public class AddNotSupportedEntityCommand<TEntity> : IAddEntityCommand<TEntity>, INotSupportedEntityCommand where TEntity : INamedEntity { }

    public class UpdateNotSupportedEntityCommand<TEntity> : IUpdateEntityCommand<TEntity>, INotSupportedEntityCommand where TEntity : INamedEntity { }

    public class DeleteNotSupportedEntityCommand<TEntity> : IDeleteEntityCommand<TEntity>, INotSupportedEntityCommand where TEntity : INamedEntity { }

}
