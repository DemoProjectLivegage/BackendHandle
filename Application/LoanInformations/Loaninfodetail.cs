using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistence;

namespace Application.LoanInformations
{
    public class Loaninfodetail
    {
          public class Query : IRequest<LoanInformation>
        {
            public int BorrowerId { get; set; }
        }

        public class Handler : IRequestHandler<Query, LoanInformation>
        {
            private readonly DatabaseContext _context;
         
            public Handler(DatabaseContext context)
            {
                _context = context;
                
            }
             public  async Task<LoanInformation> Handle(Query request, CancellationToken cancellationToken)
            {
                return await  _context.LoanInformation.FindAsync(request.BorrowerId);
            }
        }
    }
}