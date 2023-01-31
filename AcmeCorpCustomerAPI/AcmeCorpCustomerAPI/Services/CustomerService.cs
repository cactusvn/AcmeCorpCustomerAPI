using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AcmeCorpCustomerAPI.Requests;
using AcmeCorpCustomerAPI.Responses;
using AcmeCorpCustomerAPI.Interfaces;
using AcmeCorpCustomerAPI.Entities;

namespace AcmeCorpCustomerAPI.Services
{
    public class CustomerService : ICustomerService
    {
        private string secret;
        private readonly CustomersDbContext customersDbContext;
        public CustomerService(CustomersDbContext customersDbContext, IConfiguration configuration)
        {
            this.customersDbContext = customersDbContext;
            this.secret = configuration.GetValue<string>("secretKey");
        }

        public async Task<LoginResponse> Login(LoginRequest loginRequest)
        {
            var customer = customersDbContext.Customers.SingleOrDefault(customer => customer.Active && customer.Username == loginRequest.Username);

            if (customer == null)
            {
                return null;
            }
            if (customer.Password != loginRequest.Password)
            {
                return null;
            }

            var claims = new List<Claim> {
                    new Claim(ClaimTypes.PrimarySid, customer.Id.ToString()),
                    new Claim(ClaimTypes.Email, customer.Email),
                    new Claim("Role", customer.Role.Trim())
                };
            var secretKey = Encoding.ASCII.GetBytes(secret); 

            var expireAt = DateTime.Now.AddMinutes(50);
            var token = new JwtSecurityToken(
                claims: claims,
                notBefore: DateTime.Now,
                expires: expireAt,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(secretKey),
                    SecurityAlgorithms.HmacSha256Signature
                )
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return new LoginResponse { Username = customer.Username, FirstName = customer.FirstName, LastName = customer.LastName, Token = tokenString, ExpiresAt = expireAt };
        }

    }
}
