using WebShopAPI.Core.Entities;
using WebShopAPI.Helpers;

namespace WebShopAPI.Core.Interfaces
{
    public interface IProductRepository
    {
        Task<PagedResult<Product>> GetProductsAsync(int page, int pageSize);
        Task<Product> GetProductByIdAsync(Guid productId);
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(Guid productId);
    }
}
