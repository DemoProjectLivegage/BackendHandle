



using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;


public class BorrowerDetails
{
       [Key]
    public   Guid  BorrowerId {set; get;}
     // public int? ID {set; get;}
    public  string FullName {set; get;}

     public  int ContactNumber {set; get;}

      public  string MailingAddress {set; get;}

      public  int Zipcode {get; set;}

    public   string Email {get;set;}

    public  string Occupation {get;set;}
public ICollection<LoanInformation> LoanInformation {get;set;}

} 
