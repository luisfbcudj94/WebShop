using WebShopAPI.Application.DTOs;
using WebShopAPI.Helpers;

namespace WebShopAPI.Application.Interfaces
{
    public interface IProductService
    {
        Task<PagedResult<ProductDTO>> GetProductsAsync(int page, int pageSize);
        Task<ProductDTO> GetProductByIdAsync(Guid productId);
        Task<bool> AddToCartAsync(Guid orderId, OrderDTO data);
        Task<bool> UpdateProductAsync(ProductDTO productDto);
    }
}
