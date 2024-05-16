using Ecommerce.BL.Dto;
using Ecommerce.BL.Interfaces.Services;
using Ecommerce.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderServiceProxy _orderServiceProxy;

        public OrderController(IOrderServiceProxy orderServiceProxy)
        {
            _orderServiceProxy = orderServiceProxy;
        }

        [HttpPost("checkout")]
        public async Task<IActionResult> CheckoutOrder(OrderDto orderDto)
        {
            await _orderServiceProxy.CheckoutOrderAsync(orderDto);

            return Ok("Order placed successfully.");
        }

        [HttpGet]
        public async Task<IActionResult> GetOrderById(Guid orderId)
        {
            var order = await _orderServiceProxy.GetOrderByIdAsync(orderId);
            if (order == null)
            {
                return NotFound($"Order with ID {orderId} not found.");
            }
            return Ok(order);
        }

        [HttpPost("clone")]
        public async Task<IActionResult> CloneOrder(Guid orderId)
        {
            var clonedOrder = await _orderServiceProxy.CloneOrderAsync(orderId);
            if (clonedOrder == null)
            {
                return NotFound($"Order with ID {orderId} not found.");
            }
            return Ok("Order cloned successfully.");
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Order>>> GetAllOrdersByUserId()
        {
            var orders = await _orderServiceProxy.GetAllOrdersByUserIdAsync();
            return Ok(orders);
        }
    }
}
