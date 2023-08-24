using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistence;

namespace Application.Escrow_schedule
{
    public class EscrowDisbursementScheduleService
    {
        public class Query : IRequest<Escrow_Disbursement_Schedule> {
            public int loanId {get; set;}
        }
        public class Handler : IRequestHandler<Query, Escrow_Disbursement_Schedule>
        {
            private readonly DatabaseContext context;
            public Handler(DatabaseContext context)
            {
                this.context = context;
            }

            public async Task<Escrow_Disbursement_Schedule> Handle(Query request, CancellationToken cancellationToken)
            {

                
                return await this.context.Escrow_Disbursement.FindAsync(request.loanId);
            }
        }
    }
}