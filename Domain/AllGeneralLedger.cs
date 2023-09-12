using System.Transactions;

namespace Domain
{
    public class AllGeneralLedger
    {
        public int Id { get; set; }
        public int LoanId { get; set; }
        public decimal from_account_balance { get; set; }
        public decimal to_account_balance { get; set; }
        public int TransactionId { get; set; }
        public Transactions transaction {set; get;}
    }
}