namespace Domain
{
    public class GeneralLedger
    {
      
        public int ID { get; set; }
        public int account_no { get; set; } // account number for specific GL
        public string name { get; set; } //Principal collected
        public string type { get; set; } // credit/debit
        public string operation { get; set; } // sum, difference

        public string description { get; set; } //specific detail of disbursing payment in a head
        public int coa_id{get; set; }
       public COA COA { get; set; } = null;
    }
}

