using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistence;

namespace Application.LoansDetails
{
    public class Loandetail
    {
         public class Query:IRequest<LoanDetails>
        {
          public   int LoanInformationId {get; set;}
        }

        public class Handler : IRequestHandler<Query, LoanDetails>
        {  

          private readonly  DatabaseContext _context;
            public Handler(DatabaseContext context)
            {
               _context=context; 
            }
            public async Task<LoanDetails> Handle(Query request, CancellationToken cancellationToken)
            {
               return await _context.LoanDetails.FindAsync(request.LoanInformationId);
            }
        }
    }
}