using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class EscrowBeneficiaryDTO
    {
        public string escrow_type {set; get;}
        public string name {set; get;}
        public string account_no {set; get;}
        public string routing_no {set; get;}
        public string payment_mode {set; get;}
        public string frequency{set; get;}
    }
}