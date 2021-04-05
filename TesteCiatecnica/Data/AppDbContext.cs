using Microsoft.EntityFrameworkCore;
using TesteCiatecnica.Models.Entities;

namespace TesteCiatecnica.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        { }
        
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new CustomerMap(modelBuilder.Entity<Customer>());
            new AddressMap(modelBuilder.Entity<Address>());
        }
    }
}
