using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistence;

namespace Application.Borrower
{
    public class Details
    {
         public class Query : IRequest<BorrowerDetails>{
            public int BorrowerId  { get; set; }
        }

        public class Handler : IRequestHandler<Query, BorrowerDetails>
        {
            private readonly DatabaseContext _context;
           public Handler (DatabaseContext context)
           {
               _context=context;
           }
            public  async Task<BorrowerDetails> Handle(Query request, CancellationToken cancellationToken)
            {
                return await  _context.BorrowersDetails.FindAsync(request.BorrowerId);
            }
        }
    }
    }
