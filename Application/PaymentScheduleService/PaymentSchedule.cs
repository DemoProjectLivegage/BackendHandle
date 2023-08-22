using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Persistence;

namespace Application.PaymentScheduleService
{
    public class PaymentSchedule
    {
        public class Query : IRequest<List<Payment_Schedule>> {

        }
        public class Handler : IRequestHandler<Query, List<Payment_Schedule>>
        {
            private readonly DatabaseContext context;
            public Handler(DatabaseContext context)
            {
                this.context = context;
            }

            public async Task<List<Payment_Schedule>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await this.context.Payment_Schedule.ToListAsync();
            }
        }
    }
}