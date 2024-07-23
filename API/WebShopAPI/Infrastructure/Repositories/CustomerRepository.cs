using Microsoft.EntityFrameworkCore;
using WebShopAPI.Core.Entities;
using WebShopAPI.Core.Interfaces;
using WebShopAPI.Infrastructure.Data;

namespace WebShopAPI.Infrastructure.Repositories
{
    public class CustomerRepository: ICustomerRepository
    {
        private readonly AppDbContext _context;

        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer> GetCustomerByIdAsync(Guid customerId)
        {
            return await _context.Customers.FindAsync(customerId);
        }

        public async Task AddCustomerAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCustomerAsync(Guid customerId)
        {
            var customer = await _context.Customers.FindAsync(customerId);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
        }
    }
}
