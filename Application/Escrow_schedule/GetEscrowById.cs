using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Persistence;

namespace Application.Escrow_schedule
{
    public class GetEscrowById
    {
        public class Query : IRequest<List<Escrow_Disbursement_Schedule>> {
            public int Id {get; set;}
        }
        public class Handler : IRequestHandler<Query, List<Escrow_Disbursement_Schedule>>
        {
            public DatabaseContext context { get; }
            public Handler(DatabaseContext context)
            {
                this.context = context;
            }

            public async Task<List<Escrow_Disbursement_Schedule>> Handle(Query request, CancellationToken cancellationToken)
            {
                Payment_Schedule newList = await this.context.Payment_Schedule.FindAsync(request.Id);
                if(!newList.Escrow) return null;
                
                List<Escrow_Disbursement_Schedule> list = await this.context.Escrow_Disbursement_Schedule.ToListAsync();
                // list.OrderBy(a => a.Loan_Id).GroupBy(x => x.Loan_Id);
                // list.OrderBy(x => x.date);
                // var byId = list.Find(x => x.Loan_Id == request.Id);
                list = list.FindAll(a=>a.Loan_Id==request.Id).ToList();
                return list;
            }
        }
    }
}