using Application.DTO;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Dashboard
{
    public class BarChartDetails
    {
        public class Query : IRequest<List<BarDTO>>
        {
            public int LoanId { get; set; }
        }

        public class Handler : IRequestHandler<Query, List<BarDTO>>
        {
            protected readonly DatabaseContext _context;
            private readonly IMapper _mapper;
            public Handler(DatabaseContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;

            }
            public async Task<List<BarDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                List<Payment_Schedule> payments = await _context.Payment_Schedule.Where(x => x.Loan_Id == request.LoanId).ToListAsync();
                List<BarDTO> response =  _mapper.Map<List<BarDTO>>(payments);
                return response;
            }
        }
    }
}