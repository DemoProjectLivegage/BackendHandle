
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Borrower
{
    public class List
    {

      public class Query : IRequest<List<BorrowerDetails>>{ }

        public class Handler : IRequestHandler<Query, List<BorrowerDetails>>
        { 
            private readonly DatabaseContext _context;
           
            public Handler(DatabaseContext context)
            {
                _context=context;
              
            }
            public async Task<List<BorrowerDetails>> Handle(Query request, CancellationToken cancellationToken)


            {
 
                var borrowerDetails= await _context.BorrowersDetails.ToListAsync();
          
               return borrowerDetails;
           
        }
                
        }
    }
}