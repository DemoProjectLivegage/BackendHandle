using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistence;

namespace Application.PaymentScheduleService
{
    public class PaymentScheduleById
    {
        public class Query : IRequest<List<Payment_Schedule>> {
            public int Loan_Id {set; get;}
        }
        public class Handler : IRequestHandler<Query, List<Payment_Schedule>>
        {
            public DatabaseContext _context;
            public Handler(DatabaseContext context)
            {
                _context = context;
            }

            public Task<List<Payment_Schedule>> Handle(Query request, CancellationToken cancellationToken)
            {
                 var result = from schedule in _context.Payment_Schedule
                                join loan in _context.LoanDetails
                                on schedule.Loan_Id equals loan.LoanId
                                select new Payment_Schedule {
                                    Id = schedule.Id,
                                    Loan_Id = schedule.Loan_Id,
                                    Due_Date = schedule.Due_Date,
                                    Principal_Amount = schedule.Principal_Amount,
                                    Interest_Amount = schedule.Interest_Amount,
                                    Monthly_Payment_Amount = schedule.Monthly_Payment_Amount,
                                    Escrow_Amount = schedule.Escrow_Amount,
                                    UPB_Amount = schedule.UPB_Amount,
                                };

                List<Payment_Schedule> payments = new List<Payment_Schedule>();

                foreach (var item in result) 
                {
                    if(item.Loan_Id == request.Loan_Id) payments.Add(item);
                }

                return Task.FromResult(payments);
            }
        }
    }
}