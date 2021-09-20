using MediatR;
using System;
using FluentResults;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.Asanate.Application.Credits.Commands {
    public class CreateInitiativeCommand {

        public class Command : IRequest<Result> {
            public int? ProjectId { get; set; }

            public string? ExternalId { get; set; }

            public string Name { get; set; }

        }
    }
}







//using Application.Interfaces.Persistance;
//using FluentResults;
//using MediatR;
//using Microsoft.EntityFrameworkCore;
//using Olive.LRP.Domain;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Application.Credits.Commands {
//    public class GrantCreditsCommand {

//        public class Command : IRequest<Result> {
//            public int MemberId { get; set; }

//            public CreditGrantType GrantType { get; set; }

//            public DateTime ExpirationDate { get; set; }

//            public int CreditAmount { get; set; }

//        }

//        public class DuplicateDetectionError : Error {
//            public DuplicateDetectionError() : base("Unable to add CreditGrant with idenitcal properties.") { }
//        }

//        public class Handler : RequestHandler<Command, Result> {

//            private readonly IMemberRepository _memberRepository;
//            private readonly ICreditGrantRepository _creditGrantRepository;
//            private readonly ICreditTransactionRepository _creditTransactionRepository;

//            public Handler(IMemberRepository memberRepository,
//                           ICreditGrantRepository creditGrantRepository,
//                           ICreditTransactionRepository creditTransactionRepository) {
//                _memberRepository = memberRepository;
//                _creditGrantRepository = creditGrantRepository;
//                _creditTransactionRepository = creditTransactionRepository;
//            }

//            protected override Result Handle(Command command) {

//                //TODO: Make sure all changes are part of a single batch and roll back on failure

//                try {
//                    var member = _memberRepository.GetOrCreate(command.MemberId);

//                    var creditGrant = new CreditGrant {
//                        MemberId = command.MemberId,
//                        ExpirationDate = command.ExpirationDate,
//                        GrantType = command.GrantType,
//                        CreditBalance = command.CreditAmount,
//                    };

//                    _creditGrantRepository.Add(creditGrant);
//                    _creditGrantRepository.Save();

//                    // Takes the current balance of all existing CreditGrants and adds this one.
//                    var balanceOfAllCredits = member.CreditGrants.Select(cg => cg.CreditBalance).Sum();

//                    var creditTransaction = new CreditTransaction {
//                        CreditGrant = creditGrant,
//                        TransactionDate = DateTime.Now,  //TODO: This should be populated by the database
//                        CreditTransactionAmount = creditGrant.CreditBalance,
//                        TransactionType = TransactionType.Grant,
//                        Member = member
//                    };

//                    _creditTransactionRepository.Add(creditTransaction);
//                    _creditTransactionRepository.Save();
//                    member.IsActive = true;
//                    member.CreditBalance = balanceOfAllCredits;
//                    _memberRepository.Save();

//                    return Result.Ok();
//                } catch (DbUpdateException dbEx) {

//                    var errorMessage = dbEx.InnerException.Message;

//                    // SQL: "Cannot insert duplicate key row in object 'dbo.CreditGrants' with unique index
//                    // 'IX_CreditGrants_MemberId_GrantType_ExpirationDate_CreditBalance'. 

//                    // SQLite: "SQLite Error 19: 'UNIQUE constraint failed:
//                    // CreditGrants.MemberId, CreditGrants.GrantType, CreditGrants.ExpirationDate, CreditGrants.CreditBalance'."

//                    if (errorMessage.ToLower().Contains("unique"))
//                        return Result.Fail(new DuplicateDetectionError());

//                    return Result.Fail(new Error(dbEx.Message));
//                }

//            }
//        }


//    }
//}
