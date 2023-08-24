using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTO;
using Domain;

namespace API.Controllers.DTO
{
    public class LoanBeneficiaryDTO
    {
        public int loanId {get; set;}
        public BeneficiaryDTO beneficiary_1 {get; set;}
        public BeneficiaryDTO beneficiary_2 {get; set;}
        public BeneficiaryDTO beneficiary_3 {get; set;}
        public BeneficiaryDTO beneficiary_4 {get; set;}
        public BeneficiaryDTO beneficiary_5 {get; set;}
    }
}