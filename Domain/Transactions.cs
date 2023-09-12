namespace Domain
{
    public class Transactions
    {
        public int Id { get; set; }
        public string transaction_name {get;set;}

        public int? from_account { get; set; }
        public int? to_account { get; set; }
        public GeneralLedger from_GeneralLedger { get; set; }=null;
        public GeneralLedger to_generalLedger { get; set; }=null;
    }
}