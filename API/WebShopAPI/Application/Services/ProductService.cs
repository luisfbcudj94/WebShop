using WebShopAPI.Application.DTOs;
using WebShopAPI.Application.Interfaces;
using WebShopAPI.Core.Interfaces;
using WebShopAPI.Helpers;

namespace WebShopAPI.Application.Services
{
    public class ProductService: IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IOrderService _orderService;

        public ProductService(IProductRepository productRepository, IOrderService orderService)
        {
            _productRepository = productRepository;
            _orderService = orderService;
        }

        public async Task<PagedResult<ProductDTO>> GetProductsAsync(int page, int pageSize)
        {
            var result = await _productRepository.GetProductsAsync(page, pageSize);

            return new PagedResult<ProductDTO>
            {
                Items = result.Items.Select(p => new ProductDTO
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    ProductCode = p.ProductCode,
                    Description = p.Description,
                    Price = p.Price,
                    StockQuantity = p.StockQuantity,
                    ImageBase64 = p.ImageBase64
                }),
                HasPreviousPage = result.HasPreviousPage,
                HasNextPage = result.HasNextPage,
                TotalCount = result.TotalCount
            };
        }

        public async Task<ProductDTO> GetProductByIdAsync(Guid productId)
        {
            var product = await _productRepository.GetProductByIdAsync(productId);
            if (product == null) return null;

            return new ProductDTO
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                ProductCode = product.ProductCode,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                ImageBase64 = product.ImageBase64
            };
        }

        public async Task<bool> AddToCartAsync(Guid orderId, Guid productId, Guid customerId, int quantity)
        {
            // Implementation to add product to cart
            var product = await _productRepository.GetProductByIdAsync(productId);
            if (product == null || product.StockQuantity < quantity) return false;

            return await _orderService.AddProductToCartAsync(orderId, productId, customerId, quantity);
        }

        public async Task<bool> UpdateProductAsync(ProductDTO productDto)
        {
            var product = await _productRepository.GetProductByIdAsync(productDto.ProductId);
            if (product == null) return false;

            product.ProductName = productDto.ProductName;
            product.ProductCode = productDto.ProductCode;
            product.Description = productDto.Description;
            product.Price = productDto.Price;
            product.StockQuantity = productDto.StockQuantity;
            product.ImageBase64 = productDto.ImageBase64;

            await _productRepository.UpdateProductAsync(product);
            return true;
        }
    }
}
