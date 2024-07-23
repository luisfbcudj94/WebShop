using WebShopAPI.Core.Entities;

namespace WebShopAPI.Core.Interfaces
{
    public interface IOrderProductRepository
    {
        Task<IEnumerable<OrderProduct>> GetOrderProductsAsync();
        Task<OrderProduct> GetOrderProductByIdAsync(Guid orderProductId);
        Task AddOrderProductAsync(OrderProduct orderProduct);
        Task UpdateOrderProductAsync(OrderProduct orderProduct);
        Task DeleteOrderProductAsync(Guid orderProductId);
    }
}
