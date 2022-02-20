using Gringotts.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gringotts.DAL
{
    public class BankDbContext: DbContext
    {
        public BankDbContext(DbContextOptions<BankDbContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            BuildIndexes(modelBuilder);
        }

        private void BuildIndexes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasKey(x => x.Id);
            modelBuilder.Entity<Customer>()
            .Property(x => x.CreatedAt)
            .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Customer>()
                    .Property(x => x.UpdatedAt)
                    .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Account>().HasKey(x => x.AccountNumber);
            modelBuilder.Entity<Account>().HasIndex(x => x.CustomerId);
            modelBuilder.Entity<Account>()
            .Property(x => x.CreatedAt)
            .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Account>()
                    .Property(x => x.UpdatedAt)
                    .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Transaction>().HasKey(x => x.Id);
            modelBuilder.Entity<Transaction>().HasIndex(x => x.AccountNumber);
            modelBuilder.Entity<Transaction>()
            .Property(x => x.Time)
            .HasDefaultValueSql("getdate()");
        }
    }
}
