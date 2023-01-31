using AcmeCorpCustomerAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AcmeCorpCustomerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService orderService;

        public OrdersController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpGet]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> Get()
        {
            var claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;

            var claim = claimsIdentity.FindFirst(ClaimTypes.PrimarySid);
            
            if (claim == null)
            {
                return Unauthorized("Invalid customer");
            }

            var orders = await orderService.GetOrdersByCustomerId(int.Parse(claim.Value));

            if (orders == null || !orders.Any())
            {
                return BadRequest($"No order was found");
            }

            return Ok(orders);
        }
    }
}
