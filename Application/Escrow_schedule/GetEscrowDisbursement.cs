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
    public class GetEscrowDisbursement
    {
        public class Query : IRequest<List<Escrow_Disbursement_Schedule>> {}
        public class Handler : IRequestHandler<Query, List<Escrow_Disbursement_Schedule>>
        {
            public DatabaseContext context;
            public Handler(DatabaseContext context)
            {
                this.context = context;
            }

            public async Task<List<Escrow_Disbursement_Schedule>> Handle(Query request, CancellationToken cancellationToken)
            {
                List<Escrow_Disbursement_Schedule> list = await this.context.Escrow_Disbursement_Schedule.ToListAsync();
                list.OrderBy(x => x.date).GroupBy(a => a.Loan_Id);

                return list;
            }
        }
    }
}