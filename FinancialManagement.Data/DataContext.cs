using Microsoft.EntityFrameworkCore;
using FinancialManagement.Core.Entities;

namespace FinancialManagement.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Donation> Donations { get; set; }
        public DbSet<Deposit> Deposits { get; set; }
        public DbSet<UserGuarantee> Guarantees { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<GlobalVariables> GlobalVariables { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=FinancialManagement2");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Loan>()
                .HasMany(l => l.Guarantees)
                .WithOne(g => g.Loan)
                .HasForeignKey(g => g.LoanId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Loan>()
                .HasMany(l => l.DepositGuarantee)
                .WithOne(g => g.Loan)
                .HasForeignKey(g => g.LoanId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
