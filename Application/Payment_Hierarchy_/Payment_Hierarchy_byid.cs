using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistence;

namespace Application.Payment_Hierarchy_
{
    public class Payment_Hierarchy_byid
    {
        public class Query : IRequest<Payment_Hierarchy>
        {
            public int Id {get; set;}
        }

        public class Handler : IRequestHandler<Query, Payment_Hierarchy>
        {
            private readonly DatabaseContext _context;

            public Handler(DatabaseContext context)
            {
                _context = context;
            }

            public async Task<Payment_Hierarchy> Handle(Query request, CancellationToken cancellationToken)
            {    
                return await _context.Payment_Hierarchy.FindAsync(request.Id);
            }
        }
    }
}