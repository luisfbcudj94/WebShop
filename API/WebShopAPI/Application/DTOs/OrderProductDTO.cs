namespace WebShopAPI.Application.DTOs
{
    public class OrderProductDTO
    {
        public Guid OrderProductId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
