using Microsoft.EntityFrameworkCore;
using Domain;

namespace Persistence
{
    public class DatabaseContext :DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<BorrowerDetails>  Borrowers {set; get; }

        public DbSet<LoanDetails> Loan_Details {set; get;}

        public DbSet<LoanInformation> Loan_Information {get; set;}

    }
}