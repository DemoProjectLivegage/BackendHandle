using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Domain;
using Persistence;

namespace Application.Borrower
{
    public class LoanDetailsAPI
    {
        public class Query : IRequest<LoanDetails>
        {
            public int LoanId { get; set; }
        }

        public class Handler : IRequestHandler<Query, LoanDetails>
        {
            private readonly DatabaseContext _context;
            public Handler(DatabaseContext context)
            {
                _context = context;
            }
            public async Task<LoanDetails> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.LoanDetails.FindAsync(request.LoanId);
            }
        }
    }
}