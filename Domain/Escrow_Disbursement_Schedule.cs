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
        public int loan_id {get; set;}

        public DateOnly date {get; set;}

        public decimal escrow_payment_amount {get; set;}

        public decimal escrow_name {get; set;}
        public decimal escrow_disbursement {get; set;}

        public string disbursement_frequency {get; set;}

        public decimal escrow_balance {get; set;}
    }
}