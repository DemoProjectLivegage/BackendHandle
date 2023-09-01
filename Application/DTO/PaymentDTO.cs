using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class PaymentDTO
    {
        public DateOnly Due_Date {get; set;}

        public string Principal_Amount {get; set;}

        public string Interest_Amount {get; set;}

        public string Escrow_Amount {get; set;}
        
        public string Monthly_Payment_Amount {get; set;}

        public string UPB_Amount {get; set;}
    }
}