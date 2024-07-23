using WebShopAPI.Application.DTOs;
using WebShopAPI.Application.Interfaces;
using WebShopAPI.Core.Entities;
using WebShopAPI.Core.Interfaces;

namespace WebShopAPI.Application.Services
{
    public class OrderService: IOrderService
    {
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderProductRepository _orderProductRepository;
        private readonly ICustomerRepository _customerRepository;

        public OrderService(
            IProductRepository productRepository,
            IOrderRepository orderRepository, 
            IOrderProductRepository orderProductRepository, 
            ICustomerRepository customerRepository)
        {
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _orderProductRepository = orderProductRepository;
            _customerRepository = customerRepository;
        }

        public async Task<IEnumerable<OrderDTO>> GetOrdersAsync()
        {
            var orders = await _orderRepository.GetOrdersAsync();
            // Convert to DTOs
            return orders.Select(o => new OrderDTO
            {
                OrderId = o.OrderId,
                CustomerId = o.CustomerId,
                OrderDate = o.OrderDate,
                OrderProducts = o.OrderProducts.Select(op => new OrderProductDTO
                {
                    OrderProductId = op.OrderProductId,
                    ProductId = op.ProductId,
                    Quantity = op.Quantity
                }).ToList()
            });
        }

        public async Task<OrderDTO> GetOrderByIdAsync(Guid orderId)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order == null) return null;

            return new OrderDTO
            {
                OrderId = order.OrderId,
                CustomerId = order.CustomerId,
                OrderDate = order.OrderDate,
                OrderProducts = order.OrderProducts.Select(op => new OrderProductDTO
                {
                    OrderProductId = op.OrderProductId,
                    ProductId = op.ProductId,
                    Quantity = op.Quantity
                }).ToList()
            };
        }
        public async Task<bool> AddProductToCartAsync(Guid orderId, Guid productId, Guid customerId,int quantity)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);

            if (order == null)
            {
                order = new Order
                {
                    OrderId = orderId,
                    CustomerId = customerId,
                    OrderDate = DateTime.UtcNow
                };

                await _orderRepository.AddOrderAsync(order);
            }

            var product = await _productRepository.GetProductByIdAsync(productId);
            if (product == null) throw new Exception("Product not found");

            if (product.StockQuantity < quantity) throw new Exception("Not enough stock available");

            var orderProduct = new OrderProduct
            {
                OrderProductId = Guid.NewGuid(),
                OrderId = order.OrderId,
                ProductId = product.ProductId,
                Quantity = quantity
            };

            await _orderProductRepository.AddOrderProductAsync(orderProduct);

            product.StockQuantity = product.StockQuantity - quantity;
            await _productRepository.UpdateProductAsync(product);

            return true;
        }

        public OrderDTO GetCart()
        {
            return new OrderDTO(); 
        }

        public async Task<bool> UpdateCartAsync(OrderDTO orderDto)
        {
            return true;
        }

        public async Task<bool> ProcessOrderAsync(OrderDTO orderDto)
        {
            return true;
        }
    }
}
