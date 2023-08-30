using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.VisualBasic;

namespace Domain
{
    public class Payment_Hierarchy
    {
        [Key]
       [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id {set; get;}
       [ForeignKey("{Payment_Schedule}")]
        public int Loan_id {set; get;}
        
        // [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateOnly date {set; get;}

        public decimal Monthly_Payment_Amount {set; get;}
        public decimal interest  {set; get;}
        public decimal principal {set; get;}   
        public decimal escrow {set; get;}   
        public decimal threshold {set; get;}
        public decimal actual_receive {set; get;}
        public decimal late_charge {set; get;}   
        public decimal other_fee {set; get;}   
        public decimal suspence {set; get;}   
        public decimal UPB_Amount {set; get;}   
    }
}