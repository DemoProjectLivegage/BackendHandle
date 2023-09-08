namespace Domain
{
    public class COA
    {
        public int COAID { get; set; }
        
        public string coa_name { get; set; }
        public virtual ICollection<GeneralLedger> gl_list  { get;  } = new List<GeneralLedger>();
        //  public int coa_id{get; set; }
       public Transaction Transaction { get; set; } = null;
    }
}
