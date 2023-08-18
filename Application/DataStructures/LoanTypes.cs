using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DataStructures
{
    public class LoanTypes
    {
         //Borrower Details Table
                
                    public string FullName { set; get; }
                    public string ContactNumber { set; get; }
                    public string MailingAddress { set; get; }
                    public int Zipcode { get; set; }
                    public string Email { get; set; }
                    public string Occupation { get; set; }


                    //Loan Details Table
                    public decimal PIPmtAmt { get; set; }
                    public decimal UPBAmt { get; set; }
                    public int RemainingPayments { get; set; }
                    public DateOnly PmtDueDate { get; set; }
                    public required string PropertyAddress { get; set; }


                    //Loan Information Table

                    public int PriorServicerLoanId { get; set; }
                    public DateOnly NoteDate { get; set; }  
                    public DateOnly LoanBoardingDate { get; set; }
                    public decimal NoteRatePercent { get; set; }
                    public bool Escrow { get; set; }
                    public decimal TaxInsurancePmtAmt { get; set; }
                    public decimal TotalLoanAmount { get; set; }
                    public int LoanTerm { get; set; }
                    public string LoanType { get; set; }
                    public string PaymentFreq { get; set; }
    }
}