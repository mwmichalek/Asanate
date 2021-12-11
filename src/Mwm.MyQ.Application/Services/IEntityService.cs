using FluentResults;
using Mwm.MyQ.Application.Shared.Commands;
using Mwm.MyQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.MyQ.Application.Services {
    public interface IEntityService<TEntity> where TEntity : INamedEntity {

        Task<Result<int>> ExecuteAddCommand(IAddEntityCommand<TEntity> command);

        Task<Result<int>> ExecuteUpdateCommand(IUpdateEntityCommand<TEntity> command);

        Task<Result<int>> ExecuteDeleteCommand(IDeleteEntityCommand<TEntity> command);

    }
}
