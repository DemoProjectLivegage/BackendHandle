using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.LoansDetails
{
   
         public class List
    {

      public class Query : IRequest<List<LoanDetails>>{ }

        public class Handler : IRequestHandler<Query, List<LoanDetails>>
        { 
            private readonly DatabaseContext _context;
           
            public Handler(DatabaseContext context)
            {
                _context=context;
              
            }
            public async Task<List<LoanDetails>> Handle(Query request, CancellationToken cancellationToken)


            {
 
                var loanDetails= await _context.LoanDetails.ToListAsync();
          
               return loanDetails;
           
        }
                
        }


        
    }
    }
