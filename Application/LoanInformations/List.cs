using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Persistence;

namespace Application.LoanInformations
{
    public class List
    {
        public class Query : IRequest<List<LoanInformation>> {}

        public class Handler : IRequestHandler<Query, List<LoanInformation>>
        {  
            private readonly DatabaseContext _context;

            public Handler(DatabaseContext context)
            {
                _context=context;
            }
            public async Task<List<LoanInformation>> Handle(Query request, CancellationToken cancellationToken)
            {
              var loaninfo= await _context.LoanInformation.ToListAsync();

              return loaninfo;
            }
        }
    }
}