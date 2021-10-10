using MediatR;
using Mwm.Asanate.Application.Shared.Commands;
using Mwm.Asanate.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.Asanate.Application.Shared.Workflows {
    public class EntityCommandSuccessEvent<TEntity, IEntityCommand> : INotification where TEntity : INamedEntity {

        public IEntityCommand<TEntity> Command { get; private set; }

        public TEntity Entity { get; private set; }

        public EntityCommandSuccessEvent(TEntity entity, IEntityCommand<TEntity> command) {
            Entity = entity;
            Command = command;
        }
    }

}
