using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.GL
{
    public class GetTransaction
    {
        public class Query :IRequest<List<AllGeneralLedger>>
        {

        }
        public class Handler : IRequestHandler<Query, List<AllGeneralLedger>>
        {
        private readonly DatabaseContext _context;
            public Handler (DatabaseContext context)
            {
            _context = context;
            }
            public async Task<List<AllGeneralLedger>> Handle(Query request, CancellationToken cancellationToken)
            {
               return await _context.UserTransactions.Include(x=>x.transaction.from_GeneralLedger).ToListAsync();
            }
        }
    }
}