namespace WebShopAPI.Application.DTOs
{
    public class OrderDTO
    {
        public Guid OrderId { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderProductDTO> OrderProducts { get; set; }
    }
}
