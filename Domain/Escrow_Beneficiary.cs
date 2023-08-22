using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;

namespace Domain
{
    public class Escrow_Beneficiary
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Beneficiary_Id {get; set;}

        public string Escrow_Type {get; set;}

        public string Beneficiary_Name {get; set;}

        public string Account_Number {get; set;}

        public string Routing_Number {get; set;}

        public Beneficiary_Payment_Mode Payment_Mode {get; set;}

        public Disbursement_Frequency Frequency {get; set;}
    }
}