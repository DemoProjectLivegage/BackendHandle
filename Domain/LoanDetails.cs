using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class LoanDetails
    { 
        [Key]
        public required Guid Id {get;set;}

        public required decimal PIPmtAmt {get; set;}

        public required decimal UPBAmt {get; set;}

        public required decimal RemainingPayments {get; set;}

        public required DateOnly PmtDueDate {get; set;}

        public required string PropertyAddress {get; set;}
        
         public virtual LoanInformation LoanInformation { get; set; }
    }
}