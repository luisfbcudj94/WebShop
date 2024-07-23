using Microsoft.AspNetCore.Mvc;
using WebShopAPI.Application.Interfaces;

namespace WebShopAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly IProductService _productService;

        public CatalogController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("products")]
        public async Task<IActionResult> GetProducts(int page = 1, int pageSize = 10)
        {
            var pagedProducts = await _productService.GetProductsAsync(page, pageSize);
            return Ok(pagedProducts);
        }

        [HttpGet("products/{id}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }
    }
}
