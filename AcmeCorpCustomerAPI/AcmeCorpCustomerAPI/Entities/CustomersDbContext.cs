using AcmeCorpCustomerAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AcmeCorpCustomerAPI.Entities
{
    public class CustomersDbContext : DbContext, ICustomersDbContext
    {
        public CustomersDbContext()
        {
        }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public CustomersDbContext(DbContextOptions<CustomersDbContext> options) : base(options)
        {
        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<Customer>().ToTable("Customer");
        //    modelBuilder.Entity<Order>().ToTable("Order");
        //}
    }
}
