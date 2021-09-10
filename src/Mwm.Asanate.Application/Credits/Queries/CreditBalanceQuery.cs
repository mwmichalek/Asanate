//using Application.Interfaces.Persistance;
//using FluentResults;
//using MediatR;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Application.Credits.Queries {
//    public class CreditBalanceQuery {

//        public class Request : IRequest<Result<int>> {

//            public int MemberId { get; set; }

//        }

//        public class MemberMissingError : Error {
//            public MemberMissingError(int memberId) : base($"Member doesn't exist in system. MemberId = {memberId}") { }
//        }

//        public class Handler : RequestHandler<Request, Result<int>> {

//            private IMemberRepository _memberRepository;

//            public Handler(IMemberRepository memberRepository) => _memberRepository = memberRepository;

//            protected override Result<int> Handle(Request request) {

//                var balance = _memberRepository.GetMemberCreditBalance(request.MemberId);

//                if (balance.HasValue)
//                    return Result.Ok(balance.Value);

//                return Result.Fail(new MemberMissingError(request.MemberId));
//            }
//        }
//    }
//}
