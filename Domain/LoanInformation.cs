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
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int LoanInformationId { get; set; }

    public int PriorServicerLoanId { get; set; }
    public DateOnly NoteDate { get; set; }

    public DateOnly LoanBoardingDate { get; set; }

    [Column(TypeName = "decimal(8,2)")]
    public decimal NoteRatePercent { get; set; }

   
    public bool Escrow { get; set; }

    [Column(TypeName = "decimal(8,2)")]
    public decimal TotalLoanAmount { get; set; }

    public int LoanTerm { get; set; }

    public string LoanType { get; set; }

    public string PaymentFreq { get; set; }

     public required string PropertyAddress {get; set;}

    // [ForeignKey("BorrowerDetails.BorrowerId")]
    public int BorrowerId { get; set; }
    
  }
}