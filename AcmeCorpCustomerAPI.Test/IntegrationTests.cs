using AcmeCorpCustomerAPI.Entities;
using AcmeCorpCustomerAPI.Responses;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace AcmeCorpCustomerAPI.Test
{
    public class IntergrationTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public IntergrationTest(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }


        [Fact]
        public async Task TestGetOrder_Successfully()
        {
            var httpClient = _factory.CreateDefaultClient();
            var data = new
            {
                UserName = "user1",
                Password = "abc",
            };
            var json = JsonConvert.SerializeObject(data);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("/api/Customers/login", stringContent);
            Assert.Equal(response.StatusCode, HttpStatusCode.OK);
            
            var stringResult = await response.Content.ReadAsStringAsync();
            var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(stringResult);
            Assert.Equal(loginResponse.Username, "user1");
            Assert.NotEmpty(loginResponse.Token);

            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", loginResponse.Token);
            response = await httpClient.GetAsync("/api/Orders");
            Assert.Equal(response.StatusCode, HttpStatusCode.OK);

            stringResult = await response.Content.ReadAsStringAsync();
            var orderList = JsonConvert.DeserializeObject<IEnumerable<Order>>(stringResult);
            Assert.Equal(orderList.Count(), 2);
        }


        [Fact]
        public async Task TestGetOrder_UnAuthorised()
        {
            var httpClient = _factory.CreateDefaultClient();
            var data = new
            {
                UserName = "user2",
                Password = "cdf",
            };
            var json = JsonConvert.SerializeObject(data);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("/api/Customers/login", stringContent);
            Assert.Equal(response.StatusCode, HttpStatusCode.OK);

            var stringResult = await response.Content.ReadAsStringAsync();
            var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(stringResult);
            Assert.Equal(loginResponse.Username, "user2");
            Assert.NotEmpty(loginResponse.Token);

            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", loginResponse.Token);
            response = await httpClient.GetAsync("/api/Orders");
            Assert.Equal(response.StatusCode, HttpStatusCode.Forbidden);
        }
    }
}