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

    // [ForeignKey("BorrowerDetails.BorrowerId")]
    // public int BorrowerId;
    public int PriorServicerLoanId { get; set; }


    public DateOnly NoteDate { get; set; }
    public DateOnly LoanBoardingDate { get; set; }

    public decimal NoteRatePercent { get; set; }

    public bool Escrow { get; set; }

    public decimal TaxInsurancePmtAmt { get; set; }

    public decimal TotalLoanAmount { get; set; }

    public int LoanTerm { get; set; }

    public string LoanType { get; set; }

    //  [RegularExpression("Weekely|Biweekly|Monthly", ErrorMessage = "Invalid value.")]
    public string PaymentFreq { get; set; }

    public string PrimaryContact { get; set; }

    // [ForeignKey("BorrowerDetails.BorrowerId")]
    public int BorrowerId { get; set; }
  }
}