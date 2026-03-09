using CreditCardAppMvc.Models;

using Microsoft.EntityFrameworkCore;

namespace CreditCardAppMvc.Data
{
    public class AppDbContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Application>()
                .Property(a => a.AnnualIncome)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Application>()
                .Property(a => a.CreditLimit)
                .HasPrecision(18, 2);
        }
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Application> Applications { get; set; }

        public DbSet<CreditCard> CreditCards { get; set; }


        
    }
}