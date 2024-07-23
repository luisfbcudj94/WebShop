using Microsoft.EntityFrameworkCore;
using WebShopAPI.Core.Entities;
using WebShopAPI.Core.Interfaces;
using WebShopAPI.Infrastructure.Data;

namespace WebShopAPI.Infrastructure.Repositories
{
    public class OrderRepository: IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            return await _context.Orders
                .Include(p => p.OrderProducts)
                .ToListAsync();
        }

        public async Task<Order> GetOrderByIdAsync(Guid orderId)
        {
            return await _context.Orders
                .Include(o => o.OrderProducts)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);
        }

        public async Task AddOrderAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(Guid orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }
    }
}
