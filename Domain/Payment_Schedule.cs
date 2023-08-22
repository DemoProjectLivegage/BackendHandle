using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class Payment_Schedule
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get; set;}

        [Column(TypeName = "decimal(8,2)")]
        public decimal Monthly_Payment_Amount {get; set;}

        [Column(TypeName = "decimal(8,2)")]
        public decimal Principal_Amount {get; set;}

        [Column(TypeName = "decimal(8,2)")]
        public decimal Interest_Amount {get; set;}

        [Column(TypeName = "decimal(8,2)")]
        public decimal Tax_Amount {get; set;}

        [Column(TypeName = "decimal(8,2)")]
        public decimal Insurance_Amount {get; set;}

        public DateOnly Due_Date {get; set;}

        [Column(TypeName = "decimal(8,2)")]
        public decimal UPB_Amount {get; set;}

        [ForeignKey("LoanDetails")]
        public int Loan_Id {get; set;}

        [Column(TypeName = "decimal(8,2)")]
        public decimal Note_Interest_Rate {get; set;}

        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalLoanAmount {get; set;}

        public int Tenure {get; set;}

        public string Frequency {get; set;}

        public bool Escrow {get; set;}
    }
}