


using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Domain;
using System.ComponentModel.DataAnnotations; using System.ComponentModel.DataAnnotations.Schema; 

public class BorrowerDetails
{
  public BorrowerDetails()
  {   
  }
      [Key]     [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
       public   int  BorrowerId {set; get;}

   
   
    public  string FullName {set; get;}

     public  int ContactNumber {set; get;}

      public  string MailingAddress {set; get;}

      public  int Zipcode {get; set;}

    public   string Email {get;set;}

    public  string Occupation {get;set;}
public ICollection<LoanInformation> LoanInformation {get;set;}

} 
