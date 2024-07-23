using WebShopAPI.Core.Entities;

namespace WebShopAPI.Core.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetOrdersAsync();
        Task<Order> GetOrderByIdAsync(Guid orderId);
        Task AddOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(Guid orderId);
    }
}
