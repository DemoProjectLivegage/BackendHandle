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
        public decimal SheduledAmount {get; set;}
        public decimal ReceivedAmount {get; set;}
        public decimal InterestAmount {get; set;}
        public decimal PrincipalAmount {get; set;}
        public decimal EscrowAmount {get; set;}
        public decimal LateCharges {get; set;}
        public decimal OtherFees {get; set;}
        public decimal Suspense {get; set;}
        public decimal UPBAmount {get; set;}
    }
}