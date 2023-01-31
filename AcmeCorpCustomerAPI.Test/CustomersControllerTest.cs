using AcmeCorpCustomerAPI.Controllers;
using AcmeCorpCustomerAPI.Entities;
using AcmeCorpCustomerAPI.Requests;
using AcmeCorpCustomerAPI.Responses;
using AcmeCorpCustomerAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;

namespace AcmeCorpCustomerAPI.Test
{


    public class CustomersControllerTest
    {
        private Mock<CustomersDbContext> dbContext;
        private CustomerService customerService;
        private CustomersController controller;
        public CustomersControllerTest()
        {
            dbContext = new Mock<CustomersDbContext>();
            var builder = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
              .AddEnvironmentVariables();
            IConfigurationRoot root = builder.Build();

            var data = new List<Customer>
            {
                new Customer { Username = "user1", Password = "pwd1", Role = "Role1", Email = "user1@gmail.com", Active = true },
                new Customer { Username = "user2", Password = "pwd2", Role = "Role2", Email = "user2@gmail.com", Active = true }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Customer>>();
            mockSet.As<IQueryable<Customer>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Customer>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Customer>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Customer>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());
            
            dbContext.Setup(m => m.Customers).Returns(mockSet.Object);
            customerService = new CustomerService(dbContext.Object, root);
            controller = new CustomersController(customerService);
        }

        [Fact]
        public async void Login_Successfully()
        {
            var loginRequest = new LoginRequest { Username = "user1", Password = "pwd1" };
            var actionResult = await controller.Login(loginRequest);
            var result = actionResult as OkObjectResult;
            var loginResponse = result.Value as LoginResponse;
            Assert.Equal("user1", loginResponse.Username);
            Assert.NotEmpty(loginResponse.Token);
        }

        [Fact]
        public async void Login_Failed_Empty_UserName()
        {
            var loginRequest = new LoginRequest { Username = "", Password = "pwd1" };
            var response = await controller.Login(loginRequest);
            Assert.IsType<BadRequestObjectResult>(response);
        }


        [Fact]
        public async void Login_Failed_Empty_Password()
        {
            var loginRequest = new LoginRequest { Username = "adad", Password = "" };
            var response = await controller.Login(loginRequest);
            Assert.IsType<BadRequestObjectResult>(response);
        }

        [Fact]
        public async void Login_Failed_Empty_UserName_And_Password()
        {
            var loginRequest = new LoginRequest { Username = "", Password = "" };
            var response = await controller.Login(loginRequest);
            Assert.IsType<BadRequestObjectResult>(response);
        }

        [Fact]
        public async void Login_Failed_Invalid_Credentials()
        {
            var loginRequest = new LoginRequest { Username = "user1", Password = "jha" };
            var response = await controller.Login(loginRequest);
            Assert.IsType<BadRequestObjectResult>(response);
        }
    }
}