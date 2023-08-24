using Domain;
using Microsoft.EntityFrameworkCore;

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

        // public DbSet<PaymentSchedule> PaymentSchedule { set; get; }
        
        public DbSet<Benificiary> Benificiary { set; get; }
        // public object Activities { get; set; }

        public DbSet<Payment_Schedule> Payment_Schedule {set; get;}

        public DbSet<Escrow_Disbursement_Schedule> Escrow_Disbursement_Schedule {get; set;}

        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     modelBuilder.Entity<LoanInformation>()
        //            .HasOne<LoanDetails>(p => p.LoanDetails)
        //            .WithOne(s => s.LoanInformation);
        // }
    }
}