


using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class BorrowerDetails
{
   public BorrowerDetails()
   {
   }
   [Key]
   [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
   public int BorrowerId { set; get; }

  [Required]
   public string FullName { set; get; }

     
    [Required]   [DataType(DataType.PhoneNumber)]
   public string ContactNumber { set; get; }

  [Required] 
   public string MailingAddress { set; get; }

   [Required] [DataType(DataType.PostalCode)]
   public int Zipcode { get; set; }

   [Required] [DataType(DataType.EmailAddress)]
   public string Email { get; set; }

  [Required]
   public string Occupation { get; set; }

}
