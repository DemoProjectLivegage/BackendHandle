using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class Benificiary
    {
         [Key]
       [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
       
        public int benificiary_id {set; get;}
        public string escrow_type {set; get;}
        public string name {set; get;}
        public string account_no {set; get;}
        public int routing_no {set; get;}
        public string payment_mode {set; get;}
        public string frequency{set; get;}
    }
}