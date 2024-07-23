using WebShopAPI.Application.DTOs;

namespace WebShopAPI.Application.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDTO>> GetOrdersAsync();
        Task<OrderDTO> GetOrderByIdAsync(Guid orderId);
        Task<bool> UpdateCartAsync(OrderDTO orderDto);
        Task<bool> ProcessOrderAsync(OrderDTO orderDto);
        OrderDTO GetCart();
        Task<bool> AddProductToCartAsync(Guid orderId, Guid productId, Guid customerId, int quantity);
    }
}
