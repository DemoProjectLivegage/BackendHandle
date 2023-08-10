using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class LoanInformation
    {
         [ForeignKey("LoanDetails")]
        public required Guid Id {get; set;}

        public required int Prior_Servicer_Loan_Id {get; set;}


        public required DateOnly NoteDate {get; set;}
        public required DateOnly LoanBoardingDate{get; set;}

        public required int NoteRatePercent {get; set;}

        public required bool Escrow {get; set;}

        public required int TaxInsurancePmtAmt {get; set;}

        public required  int TotalLoanAmount {get; set;}

        public required int LoanTerm {get; set;}

        public required int LoanType {get; set;}

        public required string PaymentFreq {get; set;}

       public required string PrimaryContact {get;set;}
       
     public ICollection <BorrowerDetails> BorrowerDetails {get; set;}

      public virtual LoanDetails LoanDetails { get; set; }
    }
}