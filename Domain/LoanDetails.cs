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
        public required decimal PIPmtAmt {get; set;}

        public required decimal UPBAmt {get; set;}
        public required int RemainingPayments {get; set;}

        public required DateOnly PmtDueDate {get; set;}

       
       
        

       public int LoanInformationId {get; set;}
    //   public int LoanInformationId {get; set;}
    }
}