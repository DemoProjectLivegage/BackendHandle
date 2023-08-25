using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace Application.Escrow_schedule
{
    public class UpdateEscrowAmount
    {
        public class Command : IRequest {
            public int loanId {get; set;}
            List<Escrow_Benificiary> benificiaries {get; set;}
        }
    }
}