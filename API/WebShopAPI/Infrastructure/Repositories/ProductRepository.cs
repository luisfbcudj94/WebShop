using Microsoft.EntityFrameworkCore;
using WebShopAPI.Core.Entities;
using WebShopAPI.Core.Interfaces;
using WebShopAPI.Helpers;
using WebShopAPI.Infrastructure.Data;

namespace WebShopAPI.Infrastructure.Repositories
{
    public class ProductRepository: IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<Product>> GetProductsAsync(int page, int pageSize)
        {
            var totalCount = await _context.Products.CountAsync();
            var products = await _context.Products
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Product>
            {
                Items = products,
                HasPreviousPage = page > 1,
                HasNextPage = (page * pageSize) < totalCount,
                TotalCount = totalCount
            };
        }

        public async Task<Product> GetProductByIdAsync(Guid productId)
        {
            return await _context.Products.FindAsync(productId);
        }

        public async Task AddProductAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(Guid productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
    }
}
