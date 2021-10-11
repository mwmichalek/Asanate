using FluentResults;
using Mwm.Asanate.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.Asanate.Application.Utils {
    public static class ResultExtensions {

        public static Success ToSuccess<TEntity>(this TEntity entity, ResultAction action = ResultAction.Unknown, string msg = null)
            where TEntity : NamedEntity {
            return new EntitySuccess<TEntity>(entity, action, msg); 
        }

        public static EntitySuccess<TEntity> GetSuccess<TEntity>(this Result result) where TEntity : NamedEntity {
            return (EntitySuccess<TEntity>)result.Successes.SingleOrDefault(s => s.GetType() == typeof(EntitySuccess<TEntity>));
        }
        
    }

    public enum ResultAction {
        Add,
        Update,
        Delete,
        Find,
        Unknown
    }

    public class EntitySuccess<TEntity> : Success where TEntity : NamedEntity {

        public EntitySuccess(TEntity entity, ResultAction action, string msg = null) : base(msg) {
            Entity = entity;
            Action = action;

            var compsiteMsg = $"Successfully performed {action} on {typeof(TEntity)}, Id: {entity.Id}";
            if (msg != null) compsiteMsg += $": {msg}";
            Message = compsiteMsg;
        }

        public ResultAction Action { get; private set; }

        public TEntity Entity { get; private set; }

    }
}
