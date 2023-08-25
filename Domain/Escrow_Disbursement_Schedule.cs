using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Escrow_Disbursement_Schedule
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id {get; set;}

        [ForeignKey("Benificiary")]
        public int beneficiary_id {get; set;}

        public DateOnly date {get; set;}

        public decimal escrow_payment_amount {get; set;}
        public decimal escrow_disbursement {get; set;}

        public string Escrow_Name {get; set;}

        public decimal Escrow_Balance {get; set;}

        [ForeignKey("{LoanDetails}")]
        public int Loan_Id {get; set;}

        public string disbursement_frequency {get; set;}

    }
}