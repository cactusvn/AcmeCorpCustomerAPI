using AcmeCorpCustomerAPI.Requests;
using AcmeCorpCustomerAPI.Responses;
using System.Threading.Tasks;

namespace AcmeCorpCustomerAPI.Interfaces
{
    public interface ICustomerService
    {
        Task<LoginResponse> Login(LoginRequest loginRequest);
    }
}
