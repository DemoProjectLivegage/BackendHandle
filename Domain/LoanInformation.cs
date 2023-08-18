using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{

  public enum Frequency
{
    weekly,
    biweekly,
    monthly
}
  public class LoanInformation
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int LoanInformationId { get; set; }

    // [ForeignKey("BorrowerDetails.BorrowerId")]
    // public int BorrowerId;
   [Required]
    public int PriorServicerLoanId { get; set; }

[Required]
    public DateOnly NoteDate { get; set; }
   [Required]
    public DateOnly LoanBoardingDate { get; set; }

    [Required]
    public decimal NoteRatePercent { get; set; }

   [Required]
    public bool Escrow { get; set; }

[Required]
    public decimal TaxInsurancePmtAmt { get; set; }

   [Required]
    public decimal TotalLoanAmount { get; set; }

   [Required]
    public int LoanTerm { get; set; }

[Required]
    public string LoanType { get; set; }

    [Required]   
    public string PaymentFreq { get; set; }

     public required string PropertyAddress {get; set;}

    // [ForeignKey("BorrowerDetails.BorrowerId")]
    public int BorrowerId { get; set; }
  }
}