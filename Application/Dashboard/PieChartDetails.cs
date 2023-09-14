using Application.DTO;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Dashboard
{
    public class PieChartDetails
    {
        public class Query : IRequest<PieChartDTO>
        {
            public int LoanId { get; set; }
        }

        public class Handler : IRequestHandler<Query, PieChartDTO>
        {
            protected readonly DatabaseContext _context;
            private readonly IMapper _mapper;
            public Handler(DatabaseContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;

            }
            public async Task<PieChartDTO> Handle(Query request, CancellationToken cancellationToken)
            {
                int remainingPayments = await _context.LoanDetails
                    .Where(x => x.LoanId == request.LoanId)
                    .Select(x => x.RemainingPayments)
                    .SingleOrDefaultAsync();

                int loanTerm = await _context.LoanInformation
                    .Where(x=>x.LoanInformationId == request.LoanId)
                    .Select(x => x.LoanTerm )
                    .SingleOrDefaultAsync();
                
                int completedPayments = (loanTerm*12)-remainingPayments;

                PieChartDTO response = new PieChartDTO(){
                    totalPayments = loanTerm*12,
                    remainingPayments = remainingPayments,
                    completedPayments = completedPayments
                };

                return response;

            }
        }
    }
}
