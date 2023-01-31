using AcmeCorpCustomerAPI.Entities;
using AcmeCorpCustomerAPI.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace AcmeCorpCustomerAPI.Services
{
    public class OrderService : IOrderService
    {
        private readonly CustomersDbContext customersDbContext;

        public OrderService(CustomersDbContext customersDbContext)
        {
            this.customersDbContext = customersDbContext;
        }
        public async Task<List<Order>> GetOrdersByCustomerId(int id)
        {
            var orders = await customersDbContext.Orders.Where(order => order.CustomerId == id).ToListAsync();
        
            return orders;
        }

    }
}
