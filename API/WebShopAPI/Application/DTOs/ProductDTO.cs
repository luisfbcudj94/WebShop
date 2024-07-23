namespace WebShopAPI.Application.DTOs
{
    public class ProductDTO
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string ImageBase64 { get; set; }
    }
}
