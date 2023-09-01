using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class LoanDetailsDTO
    {
        public string PIPmtAmt { get; set; }

        public string UPBAmt { get; set; }
        public int RemainingPayments { get; set; }
        public string TaxInsurancePmtAmt { get; set; }
        public DateOnly PmtDueDate { get; set; }

    }
}