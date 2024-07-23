using Microsoft.EntityFrameworkCore;
using WebShopAPI.Core.Entities;
using WebShopAPI.Core.Interfaces;
using WebShopAPI.Infrastructure.Data;

namespace WebShopAPI.Infrastructure.Repositories
{
    public class OrderProductRepository: IOrderProductRepository
    {
        private readonly AppDbContext _context;

        public OrderProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderProduct>> GetOrderProductsAsync()
        {
            return await _context.OrderProducts.ToListAsync();
        }

        public async Task<OrderProduct> GetOrderProductByIdAsync(Guid orderProductId)
        {
            return await _context.OrderProducts.FindAsync(orderProductId);
        }

        public async Task AddOrderProductAsync(OrderProduct orderProduct)
        {
            await _context.OrderProducts.AddAsync(orderProduct);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderProductAsync(OrderProduct orderProduct)
        {
            _context.OrderProducts.Update(orderProduct);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderProductAsync(Guid orderProductId)
        {
            var orderProduct = await _context.OrderProducts.FindAsync(orderProductId);
            if (orderProduct != null)
            {
                _context.OrderProducts.Remove(orderProduct);
                await _context.SaveChangesAsync();
            }
        }
    }
}
