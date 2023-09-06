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
        public class Query : IRequest<List<COA_DTO>>
        {
        }

        public class Handler : IRequestHandler<Query, List<COA_DTO>>
        {
            private readonly DatabaseContext _context;
            private readonly IMapper _mapper ;
            public Handler(DatabaseContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;

            }
            public async Task<List<COA_DTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                List<COA> data = await _context.COA.Include(b => b.gl_list).ToListAsync();
                List<COA_DTO> response = _mapper.Map<List<COA_DTO>>(data);
                return response;
            }
        }
    }
}