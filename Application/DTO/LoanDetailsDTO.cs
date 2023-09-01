namespace Application.DTO
{
    public class LoanDetailsDTO
    {
        public int LoanId { get; set; }
        public string PIPmtAmt { get; set; }

        public string UPBAmt { get; set; }
        public int RemainingPayments { get; set; }
        public string TaxInsurancePmtAmt { get; set; }
        public string monthly_payment_amount { get; set; }
        public DateOnly PmtDueDate { get; set; }

    }
}