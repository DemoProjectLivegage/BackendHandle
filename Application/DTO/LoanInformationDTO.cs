using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class LoanInformationDTO
    {
    public int loanInformationId { get; set; }
    public int PriorServicerLoanId { get; set; }
    public DateOnly NoteDate { get; set; }
    public DateOnly LoanBoardingDate { get; set; }
    public string NoteRatePercent { get; set; }
    public bool Escrow { get; set; }
    public string TotalLoanAmount { get; set; }
    public string LoanTerm { get; set; }
    public string LoanType { get; set; }
    public string PaymentFreq { get; set; }
     public string PropertyAddress {get; set;}
    }
}