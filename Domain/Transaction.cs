using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class Transaction
    {
        public int Id { get; set; }

        public int transaction_name {get;set;}

        public ICollection<COA>  coa_gl {get; set;}
        
    }
}