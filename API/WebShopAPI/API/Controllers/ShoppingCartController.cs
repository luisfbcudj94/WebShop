using Microsoft.AspNetCore.Mvc;
using WebShopAPI.Application.DTOs;
using WebShopAPI.Application.Interfaces;

namespace WebShopAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public ShoppingCartController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public IActionResult GetCart()
        {
            var cart = _orderService.GetCart();
            return Ok(cart);
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateCart([FromBody] OrderDTO orderDto)
        {
            var result = await _orderService.UpdateCartAsync(orderDto);
            if (!result)
                return BadRequest();
            return Ok();
        }

        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout([FromBody] OrderDTO orderDto)
        {
            var result = await _orderService.ProcessOrderAsync(orderDto);
            if (!result)
                return BadRequest();
            return Ok();
        }
    }
}
