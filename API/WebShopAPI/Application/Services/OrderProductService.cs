using WebShopAPI.Application.DTOs;
using WebShopAPI.Application.Interfaces;
using WebShopAPI.Core.Interfaces;

namespace WebShopAPI.Application.Services
{
    public class OrderProductService: IOrderProductService
    {
        private readonly IOrderProductRepository _orderProductRepository;

        public OrderProductService(IOrderProductRepository orderProductRepository)
        {
            _orderProductRepository = orderProductRepository;
        }

        public async Task<IEnumerable<OrderProductDTO>> GetOrderProductsAsync()
        {
            var orderProducts = await _orderProductRepository.GetOrderProductsAsync();
            return orderProducts.Select(op => new OrderProductDTO
            {
                OrderProductId = op.OrderProductId,
                ProductId = op.ProductId,
                Quantity = op.Quantity
            });
        }

        public async Task<OrderProductDTO> GetOrderProductByIdAsync(Guid orderProductId)
        {
            var orderProduct = await _orderProductRepository.GetOrderProductByIdAsync(orderProductId);
            if (orderProduct == null) return null;

            return new OrderProductDTO
            {
                OrderProductId = orderProduct.OrderProductId,
                ProductId = orderProduct.ProductId,
                Quantity = orderProduct.Quantity
            };
        }
    }
}
