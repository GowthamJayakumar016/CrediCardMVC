using CreditCardAppMvc.Models;
using Microsoft.EntityFrameworkCore;

namespace CreditCardAppMvc.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        
       public DbSet<Application>Applications { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Application>()
                .Property(a => a.AnnualIncome)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Application>()
                .Property(a => a.CreditLimit)
                .HasPrecision(18, 2);
        }
    }
}