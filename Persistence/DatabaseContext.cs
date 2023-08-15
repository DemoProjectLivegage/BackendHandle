using Microsoft.EntityFrameworkCore;
using Domain;

namespace Persistence
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<BorrowerDetails> BorrowersDetails { set; get; }
        public DbSet<LoanInformation> LoanInformation { get; set; }
        public DbSet<LoanDetails> LoanDetails { set; get; }
        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     modelBuilder.Entity<LoanInformation>()
        //            .HasOne<LoanDetails>(p => p.LoanDetails)
        //            .WithOne(s => s.LoanInformation);
        // }
    }
}