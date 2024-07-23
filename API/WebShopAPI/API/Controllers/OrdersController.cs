using Microsoft.AspNetCore.Mvc;
using WebShopAPI.Application.DTOs;
using WebShopAPI.Application.Interfaces;
using WebShopAPI.Application.Services;

namespace WebShopAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;

        public OrdersController(IOrderService orderService, IProductService productService)
        {
            _orderService = orderService;
            _productService = productService;
        }

        [HttpPost("{orderId}/addtocart")]
        public async Task<IActionResult> AddToCart(
            Guid orderId,
            OrderDTO data)
        {
            var result = await _productService.AddToCartAsync(orderId, data);
            if (!result)
                return BadRequest();
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _orderService.GetOrdersAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(Guid id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
                return NotFound();
            return Ok(order);
        }

    }
}
