



using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;


public class BorrowerDetails
{
       [Key]
    public Guid  BorrowerId {set; get;}

    public required string FullName {set; get;}

     public required int ContactNumber {set; get;}

      public required string MailingAddress {set; get;}

      public required int Zipcode {get; set;}

    public required  string Email {get;set;}

    public required string Occupation {get;set;}
public ICollection<LoanInformation> LoanInformation {get;set;}

} 
