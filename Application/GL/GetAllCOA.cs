using Application.DTO;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using AutoMapper;

namespace Application.GL
{
    public partial class GetAllCOA
    {
        public class Query : IRequest<List<OnlyCOA>>
        {
        }

        public class Handler : IRequestHandler<Query, List<OnlyCOA>>
        {
            private readonly DatabaseContext _context;
            private readonly IMapper _mapper ;
            public Handler(DatabaseContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;

            }
            public async Task<List<OnlyCOA>> Handle(Query request, CancellationToken cancellationToken)
            {
                List<COA> data = await _context.COA.ToListAsync();
                List<OnlyCOA> response = _mapper.Map<List<OnlyCOA>>(data);
                return response;
            }
        }
    }
}