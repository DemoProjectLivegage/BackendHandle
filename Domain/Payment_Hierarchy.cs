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
        [Column(TypeName = "decimal(8,2)")]
        public decimal Monthly_Payment_Amount {set; get;}
        [Column(TypeName = "decimal(8,2)")]
        public decimal interest  {set; get;}
        [Column(TypeName = "decimal(8,2)")]
        public decimal principal {set; get;}
        [Column(TypeName = "decimal(8,2)")]   
        public decimal escrow {set; get;}  
        [Column(TypeName = "decimal(8,2)")] 
        public decimal threshold {set; get;}
        [Column(TypeName = "decimal(8,2)")]
        public decimal actual_receive {set; get;}
        [Column(TypeName = "decimal(8,2)")]
        public decimal late_charge {set; get;}
        [Column(TypeName = "decimal(8,2)")]   
        public decimal other_fee {set; get;}  
        [Column(TypeName = "decimal(8,2)")] 
        public decimal suspence {set; get;}
        [Column(TypeName = "decimal(8,2)")]   
        public decimal UPB_Amount {set; get;}   
    }
}