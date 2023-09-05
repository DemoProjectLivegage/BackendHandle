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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LoanId { get; set; }
        [Column(TypeName = "decimal(8,2)")]
        public required decimal PIPmtAmt { get; set; }
        [Column(TypeName = "decimal(8,2)")]
        public required decimal UPBAmt { get; set; }
        public required int RemainingPayments { get; set; }

        [Column(TypeName = "decimal(8,2)")]
        public decimal TaxInsurancePmtAmt { get; set; }
        public required DateOnly PmtDueDate { get; set; }
         [Column(TypeName = "decimal(8,2)")]
        public decimal monthly_payment_amount { get; set; } = 0;
        [ForeignKey("{LoanInformation}")]
        public int LoanInformationId { get; set; }
        //   public int LoanInformationId {get; set;}
    }
}