using AcmeCorpCustomerAPI.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AcmeCorpCustomerAPI.Interfaces
{
    public interface IOrderService
    {
        Task<List<Order>> GetOrdersByCustomerId(int id);
    }
}
