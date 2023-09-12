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

        public DbSet<Payment_Schedule> Payment_Schedule { set; get; }

        public DbSet<Escrow_Disbursement_Schedule> Escrow_Disbursement_Schedule { get; set; }
        public DbSet<Payment_Hierarchy> Payment_Hierarchy { get; set; }
        public DbSet<GeneralLedger> GeneralLedger { get; set; }
        public DbSet<COA> COA { get; set; }
        public DbSet<Transactions> Transaction { get; set; }
        public DbSet<AllGeneralLedger> UserTransactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLazyLoadingProxies();
            }
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<COA>()
                .HasIndex(col => col.coa_name)
                .IsUnique();

            builder.Entity<Transactions>()
                .HasIndex(col => col.transaction_name)
                .IsUnique();

            builder.Entity<Transactions>()
                .HasOne(t => t.to_generalLedger)
                .WithMany()
                .HasForeignKey(t => t.to_account);
            builder.Entity<Transactions>()
                .HasOne(t => t.from_GeneralLedger)
                .WithMany()
                .HasForeignKey(t => t.from_account)
                .IsRequired(false);

            builder.Entity<COA>()
                .HasMany(e => e.gl_list)
                .WithOne(e => e.COA)
                .HasForeignKey(e => e.coa_id)
                .IsRequired(true);
        }

    }
}