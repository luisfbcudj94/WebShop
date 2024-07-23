using WebShopAPI.Application.DTOs;

namespace WebShopAPI.Application.Interfaces
{
    public interface IOrderProductService
    {
        Task<IEnumerable<OrderProductDTO>> GetOrderProductsAsync();
        Task<OrderProductDTO> GetOrderProductByIdAsync(Guid orderProductId);
    }
}
