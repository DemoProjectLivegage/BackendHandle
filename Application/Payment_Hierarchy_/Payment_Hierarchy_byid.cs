using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Payment_Hierarchy_
{
    public class Payment_Hierarchy_byid
    {
        public class Query : IRequest<List<Payment_Hierarchy>>
        {
            public int Id {get; set;}
        }

        public class Handler : IRequestHandler<Query, List<Payment_Hierarchy>>
        {
            private readonly DatabaseContext _context;

            public Handler(DatabaseContext context)
            {
                _context = context;
            }

            public async Task<List<Payment_Hierarchy>> Handle(Query request, CancellationToken cancellationToken)
            {    
                return await _context.Payment_Hierarchy.Where(x=>x.Loan_id==request.Id).ToListAsync();
            }
        }
    }
}