using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Persistence;

namespace Application.Escrow_schedule
{
    public class UpdateEscrowAmount
    {
        public class Command : IRequest {
            public int loanId {get; set;}
            public List<Escrow_Benificiary> benificiaries {get; set;}
        }
        public class Handle : IRequestHandler<Command>
        {
            private readonly DatabaseContext context;

            public Handle(DatabaseContext context)
            {
                this.context = context;
            }

            Task<Unit> IRequestHandler<Command, Unit>.Handle(Command request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}