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
        public class Query :IRequest<List<Transactions>>
        {

        }
        public class Handler : IRequestHandler<Query, List<Transactions>>
        {
        private readonly DatabaseContext _context;
            public Handler (DatabaseContext context)
            {
            _context = context;
            }
            public async Task<List<Transactions>> Handle(Query request, CancellationToken cancellationToken)
            {
               return await _context.Transaction.ToListAsync();
            }
        }
    }
}