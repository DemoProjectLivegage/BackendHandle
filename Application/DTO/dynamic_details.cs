using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class dynamic_details
    {

        public int loan_info_id {get; set;}
        public decimal UPB_Amount {get; set;}
        public decimal RemainingPayments {get; set;}
        public DateOnly Due_Date {get; set;}
        public decimal Monthly_Payment_Amount {get; set;}
    }
}