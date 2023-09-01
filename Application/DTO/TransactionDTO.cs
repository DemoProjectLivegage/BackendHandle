using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Identity;

namespace Application.DTO
{
    public class TransactionDTO
    {
        public DateOnly TransactionDate {get; set;}
        public string SheduledAmount {get; set;}
        public string ReceivedAmount {get; set;}
        public string InterestAmount {get; set;}
        public string PrincipalAmount {get; set;}
        public string EscrowAmount {get; set;}
        public string LateCharges {get; set;}
        public string OtherFees {get; set;}
        public string Suspense {get; set;}
        public string UPBAmount {get; set;}
    }
}