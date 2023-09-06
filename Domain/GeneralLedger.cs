using System.ComponentModel;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Domain
{
    public class GeneralLedger
    {
      
        public int ID { get; set; }
        public string gl_name { get; set; } //Principal collected
        public string gl_type { get; set; } // credit/debit
        public string gl_operation { get; set; } // sum, difference
        public int coa_id{get; set; }
        public COA COA { get; set; } = null;
    }
}

