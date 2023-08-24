using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class BeneficiaryDTO
    {
        public int BeneficiaryId {get; set;}
        public DateOnly disbursementDate {get; set;}
        public decimal disbursementAmount {get; set;}
    }
}