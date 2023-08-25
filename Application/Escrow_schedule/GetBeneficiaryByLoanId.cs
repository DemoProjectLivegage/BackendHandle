using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Persistence;

namespace Application.Escrow_schedule
{
    public class GetBeneficiaryByLoanId
    {
        public class Query : IRequest<List<Benificiary>> {
            public int Id {get; set;}
        }
        public class Handler : IRequestHandler<Query, List<Benificiary>>
        {
            private readonly DatabaseContext context;

            public Handler(DatabaseContext context)
            {
                this.context = context;
            }

            public async Task<List<Benificiary>> Handle(Query request, CancellationToken cancellationToken)
            {
                List<Escrow_Disbursement_Schedule> disburse = await this.context.Escrow_Disbursement_Schedule.ToListAsync();
                // disburse.OrderBy(x=>x.Loan_Id).GroupBy(a => a.Loan_Id).ToList();
                List<Escrow_Disbursement_Schedule> newList = disburse.FindAll(x=>x.Loan_Id==request.Id).DistinctBy(x=>x.beneficiary_id).ToList();

                List<Benificiary> list = new List<Benificiary>();
                foreach (var item in newList)
                {
                        int Id = item.beneficiary_id;
                        if(Id != 0) {
                            Benificiary ben = await this.context.Benificiary.FindAsync(Id);
                            list.Add(ben);
                        }
                }

                return list;
            }
        }
    }
}