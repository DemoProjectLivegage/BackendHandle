using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Escrow_schedule
{
    public class Escrow_GetBenificiary
    {
        public class Query: IRequest<List<Benificiary>>
        {
        }
        
        public class Handler : IRequestHandler<Query, List<Benificiary>>
        {
                private readonly DatabaseContext _context;
            public Handler(DatabaseContext context)
            {
                _context=context;
            }
            public async Task<List<Benificiary>> Handle(Query request, CancellationToken cancellationToken)
            {
                Console.WriteLine("ram");
                 return await _context.Benificiary.ToListAsync();
            }
        }
    }
}