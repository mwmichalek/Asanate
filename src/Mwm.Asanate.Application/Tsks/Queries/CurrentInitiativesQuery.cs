using FluentResults;
using MediatR;
using Mwm.Asanate.Application.Interfaces.Persistance;
using Mwm.Asanate.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.Asanate.Application.Credits.Queries {
    public class CurrentInitiativesQuery {

        public class Request : IRequest<Result<List<Initiative>>> {

            //public int MemberId { get; set; }

        }

        //public class MemberMissingError : Error {
        //    public MemberMissingError(int memberId) : base($"Member doesn't exist in system. MemberId = {memberId}") { }
        //}

        public class Handler : RequestHandler<Request, Result<List<Initiative>>> {

            private IRepository<Initiative> _initiativeRepository;

            public Handler(IRepository<Initiative> initiativeRepository) => _initiativeRepository = initiativeRepository;

            protected override Result<List<Initiative>> Handle(Request request) {
                var currentInitiatives = _initiativeRepository.GetAll().ToList(); // Need to filter out starred.
                return Result.Ok(currentInitiatives);
            }
        }
    }
}
