using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;

namespace Domain
{
    public class Escrow_Disbursement_Schedule
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Escrow_Id {get; set;}

        public DateOnly date {get; set;}

        public decimal Incoming_Escrow {get; set;}

        public decimal Escrow_Disbursement {get; set;}

        public string Disbursement_Frequency {get; set;}

        public decimal Escrow_Balance {get; set;}
        public int Loan_Id {get; set;}
    }
}