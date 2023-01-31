using AcmeCorpCustomerAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace AcmeCorpCustomerAPI.Interfaces
{
    public interface ICustomersDbContext
    {
        DbSet<Customer> Customers { get; set; }
        DbSet<Order> Orders { get; set; }
    }
}