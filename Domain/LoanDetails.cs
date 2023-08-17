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
       [Key]     [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LoanId {get;set;}
      
      [Required]
        public required decimal PIPmtAmt {get; set;}

       [Required]
        public required decimal UPBAmt {get; set;}

        [Required]
        public required int RemainingPayments {get; set;}

       [Required]
        public required DateOnly PmtDueDate {get; set;}

        [Required]
        public required string PropertyAddress {get; set;}
        

       public int LoanInformationId {get; set;}
    //   public int LoanInformationId {get; set;}
    }
}