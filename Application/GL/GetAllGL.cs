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
    public class GetAllGL
    {
        public class Query: IRequest<List<GeneralLedger>>{

        }
        public class Handler : IRequestHandler<Query, List<GeneralLedger>>
        {
        private readonly DatabaseContext _context;
            public Handler(DatabaseContext context)
            {
            _context = context;
                
            }
            public async Task<List<GeneralLedger>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.GeneralLedger.ToListAsync();
            }
        }
    }
}