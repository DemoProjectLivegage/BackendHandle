using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class EscrowDisbursementDTO
    {
        public int beneficiary_id {get; set;}

        public DateOnly date {get; set;}

        public string escrow_payment_amount {get; set;}

        public string escrow_disbursement {get; set;}

        public string Escrow_Name {get; set;}

        public string Escrow_Balance {get; set;}

        public string disbursement_frequency {get; set;}
    }
}