using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain; 
namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DatabaseContext context)
        {
           if(context.BorrowersDetails.Any()) return ;
     var borrowers = new List<BorrowerDetails>
     {
        new BorrowerDetails
        {
          
            FullName="Ishan",
            ContactNumber=23567890,
            MailingAddress="New ARea1"
        },

         new BorrowerDetails
        {
            
            FullName="Ishani",
            ContactNumber=235678901,
            MailingAddress="New ARea2"
        },

         new BorrowerDetails
        {
           
            FullName="Isha",
            ContactNumber=23567894,
            MailingAddress="New ARea3"
        },

         new BorrowerDetails
        {
        
            FullName="Isnaa",
            ContactNumber=23567899,
            MailingAddress="New ARea4"
        },
     };
       await context.BorrowersDetails.AddRangeAsync(borrowers);
            await context.SaveChangesAsync();
             
    }
}
}